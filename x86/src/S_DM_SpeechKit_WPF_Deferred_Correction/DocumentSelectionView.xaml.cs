using Microsoft.Win32;
using Nuance.SpeechAnywhere;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace S_DM_SpeechKit_WPF_Deferred_Correction
{
	public partial class DocumentSelectionView : Window
	{
		public DocumentSelectionView()
		{
			InitializeComponent();
			Logon();
			Closed += DocumentSelectionView_Closed;
		}

		private void DocumentSelectionView_Closed(object sender, EventArgs e)
		{
			if (Settings.CurrentUser != null)
			{
				// The current session should be closed before closing the application.
				Logoff();
			}
		}

		private void NewDocument_Click(object sender, RoutedEventArgs e)
		{
			var newDocument =
				new Document
				{
					Id = Guid.NewGuid().ToString(),
					FieldContent = string.Empty,
					// generate the document token using security recommended algorithm.
					DocumentToken = DocumentTokenGenerator.GenerateToken()
				};

			var view = new DocumentEditorView(newDocument);
			view.ShowDialog();
		}

		private void OpenDocument_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, Settings.DocumentFolderStore));
				var fileDialog = new OpenFileDialog();
				fileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, Settings.DocumentFolderStore);
				fileDialog.Multiselect = false;
				fileDialog.Filter = "json files (*.json)|*.json";
				var result = fileDialog.ShowDialog(this);

				if (result == true)
				{
					var document = DocumentExtensions.Deserialize(fileDialog.FileName);
					var view = new DocumentEditorView(document);
					view.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Unable to open document: \n\t {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Logoff_Click(object sender, RoutedEventArgs e)
		{
			Logoff();
		}

		private void Logoff()
		{
			// Free up server resources. This should be called before closing the
			// application or when the document mode needs to be switched.
			Session.SharedSession.Close();

			Settings.CurrentUser = null;
			Logon();
		}

		private void Logon()
		{
			new LoginDialogView().ShowDialog();
			if (Settings.CurrentUser == null)
			{
				Close();
			}

			LblCurrentUserId.Content = Settings.CurrentUser ?? string.Empty;

			if (Settings.DocumentMode == DocumentMode.Correctionist)
			{
				NewDocumentBtn.Visibility = Visibility.Collapsed;
			}
			else
			{
				NewDocumentBtn.Visibility = Visibility.Visible;
			}
		}
	}
}
