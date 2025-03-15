using Microsoft.Win32;
using Nuance.SpeechAnywhere;
using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using DocumentMode = Nuance.SpeechAnywhere.DocumentMode;
using VuiController = Nuance.SpeechAnywhere.WPF.VuiController;

namespace S_DM_SpeechKit_WPF_Deferred_Correction
{
	public enum CloseReason
	{
		Corrected,
		Undefined,
		Discard
	}

	/// <summary>
	/// Manages an editable document.
	/// </summary>
	public partial class DocumentEditorView : Window
	{
		/// <summary>
		/// WPF specific VuiController.
		/// </summary>
		private VuiController _vuiController;

		/// <summary>
		/// Custom document format specifically created for this sample application to simplify usage.
		/// Your integration should provide a suitable mechanism to store documents.
		/// </summary>
		private Document _document;

		public DocumentEditorView(Document document)
		{
			InitializeComponent();

			_document = document;

			InitializeVuiSession();

			Closed += DocumentEditorView_Closed;
			Loaded += DocumentEditorView_Loaded;
		}

		private void InitializeVuiSession()
		{
			// Attach events onto the global session.
			Session.SharedSession.PlaybackStarted += Session_OnPlaybackStarted;
			Session.SharedSession.PlaybackStopped += Session_OnPlaybackStopped;

			// Any document id can be provided to the SpeechKit. Best practice is to generate a completly random
			// ID and then map this ID to documents in your integration.
			LblDocumentId.Content = _document.Id;

			// Reload text content if it exists for this document. You should be careful to load the text
			// as it was last saved as to preserve correction and auditing data.
			TbDocumentContent.Text = _document.FieldContent;

			_vuiController = new VuiController();

			// Set the recognition language. This overrides the default setting which is the current UI culture;
			_vuiController.Language = XmlLanguage.GetLanguage(Settings.SrLanguage);
		}

		private void DocumentEditorView_Closed(object sender, EventArgs e)
		{
			HandleClose(null, CloseReason.Discard);
		}

		private void DocumentEditorView_Loaded(object sender, RoutedEventArgs e)
		{
			// Set the dictatable fields.
			// While this example uses a single field for simplicity, it is possible to have multiple fields in a document.
			// In addition, reopening a document requires that the same field ID be reused.
			_vuiController.SetDocumentFieldId(TbDocumentContent, Settings.DocumentFieldId);

			// Initialize the VuiController by passing the document content control as a parameter.
			// We also pass in an integrator generated document id and document token.
			_vuiController.Open(this, _document.Id, _document.DocumentToken);

			// Selectively enable or disable controls that should allow speech recognition to occur.
			_vuiController.EnableControl(TbDocumentContent, true);

			// By default controls in the same scope will be enabled for speech recognition. We
			// should disable any controls that are not part of the document.
			_vuiController.EnableControl(TbAudioLength, false);

			// Synchronize must be called after enabling/disabling controls.
			_vuiController.Synchronize();

			// The VuiController instance needs to be set as focused.
			// This should always be done after opening the session.
			_vuiController.Focused = true;
		}

		private void HandleClose(string documentId, CloseReason reason)
		{
			// Detach event fromt he global object.
			Session.SharedSession.PlaybackStarted -= Session_OnPlaybackStarted;
			Session.SharedSession.PlaybackStopped -= Session_OnPlaybackStopped;

			// For auditing purposes the correction state should be fed into the Close() method when
			// the user is finished with the document.

			switch (reason)
			{
				case CloseReason.Corrected:
					// Pass the document id along with the corrected state after correction.
					_vuiController.Close(documentId, DocumentStates.Corrected);
					break;

				case CloseReason.Discard:
					// When discarding the users current work no document id should be passed
					// to the close method. Here we also set the document state to Uncorrected;
					// If the user has however already corrected the document, then this should
					// be set to Corrected instead.
					// We should also set to uncorrected when sending a new document from the Author.
					_vuiController.Close(string.Empty, DocumentStates.Uncorrected);
					break;

				case CloseReason.Undefined:
					// Use this mode if the document state can't be determined easily.
					_vuiController.Close(documentId, DocumentStates.Undefined);
					break;

				default:
					throw new NotImplementedException("Unknown reason given for closing the document");
			}
		}

		private bool SaveDocument()
		{
			try
			{
				Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, Settings.DocumentFolderStore));
				var fileDialog = new SaveFileDialog();
				fileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, Settings.DocumentFolderStore);
				fileDialog.DefaultExt = ".json";
				fileDialog.Filter = "json files (*.json)|*.json";
				string documentId = LblDocumentId.Content.ToString();
				fileDialog.FileName = $"{documentId.Substring(0, Math.Min(8, documentId.Length))}.json";

				var result = fileDialog.ShowDialog(this);

				if (result == true)
				{
					var document = new Document
					{
						Id = LblDocumentId.Content.ToString(),
						FieldContent = TbDocumentContent.Text,
						DocumentToken = _document.DocumentToken
					};

					// Serialize a copy of our document. Your integration should provide its own mechanism to save document data.
					DocumentExtensions.Serialize(fileDialog.FileName, document);
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Unable to save document: \n\t {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return false;
		}

		private void SaveDocument_Click(object sender, RoutedEventArgs e)
		{
			if (SaveDocument())
			{
				if (Settings.DocumentMode == DocumentMode.Correctionist)
				{
					HandleClose(LblDocumentId.Content.ToString(), CloseReason.Corrected);
				}
				else
				{
					HandleClose(LblDocumentId.Content.ToString(), CloseReason.Undefined);
				}
				Close();
			}
		}

		private void DiscardDocument_Click(object sender, RoutedEventArgs e)
		{
			HandleClose(LblDocumentId.Content.ToString(), CloseReason.Discard);
			Close();
		}

		private void GetAudioLengthBtn_Click(object sender, RoutedEventArgs e)
		{
			long audioLength = _vuiController.GetAudioLength();
			TbAudioLength.Text = audioLength.ToString();
		}

		private void Session_OnPlaybackStarted()
		{
			CbPlaybackStopped.IsChecked = false;
			CbPlaybackStarted.IsChecked = true;
		}

		private void Session_OnPlaybackStopped()
		{
			CbPlaybackStarted.IsChecked = false;
			CbPlaybackStopped.IsChecked = true;
		}
	}
}
