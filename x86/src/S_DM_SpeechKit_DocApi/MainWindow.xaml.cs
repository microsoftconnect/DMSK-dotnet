using Microsoft.Win32;
using Nuance.SpeechAnywhere;
using Nuance.SpeechAnywhere.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S_SpeechAnywhereDocApi
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Convenience defines used for opening the ISession instance and pass it the licensing 
		// information needed. Your partner GUID, organization token and serviceUrl will be made available to you via the 
		// Nuance order desk Welcome Kit. 
		private const string _partnerGuid = "ENTER_YOUR_PARTNER_GUID";
		private const string _organizationToken = "ENTER_YOUR_ORGANIZATION_TOKEN";
		private const string _serviceUrl = "ENTER_SERVICE_URL";

		private VuiController _vuiController;

		private Dictionary<string, Document> _documents;

		public MainWindow()
		{
			InitializeComponent();

			_vuiController = new VuiController();
			_documents = new Dictionary<string, Document>();
		}

		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			// We are using region specific url as mentioned above
			Session.SharedSession.ServiceURL = _serviceUrl;
			Session.SharedSession.Open(_userName.Text, _organizationToken, _partnerGuid, "S_DM_SpeechKit_DocApi");

			Closing += (_, args) =>
			{
				// Close the session. 
				Session.SharedSession.Close();
			};

			// Set the recognition language. This overrides the default setting which is the current UI culture;
			_vuiController.Language = XmlLanguage.GetLanguage("en-us");
			_vuiController.Topic = "GeneralDictation";

			// On the VUI controller, set the AutoFocus property to false. This enables explicit control over the focus via the Focused property and 
			// disables the legacy behavior of implicitly activating the VUI controller instance whenever Initialize(), Open() or Synchronize() is called.
			// We will set the focus to the VUI controller whenever the window is activated (see Window_Activated below).
			_vuiController.AutoFocus = false;

			// Add event handler for VUI controller event CommandRecognized
			_vuiController.CommandRecognized += new CommandRecognized(vuiController_CommandRecognized);

			// Enable command and control for the main window
			// 1) Disable text fields (no text but only commands should be recognized)
			_vuiController.EnableControl(_userName, false);
			_vuiController.EnableControl(_documentId, false);
			// 2) Create a set of commands for command and control functionality
			// Create a single element array for the command set
			CommandSet[] commandSet = new CommandSet[1];
			// Create command set with ID and description
			commandSet[0] = new CommandSet("ReportCommandSet", "Commands for opening reports");
			// Create commands to create and to open a report
			commandSet[0].CreateCommand("newreport", "new report", "New report", "Create a new report.");
			commandSet[0].CreateCommand("openreport", "open report", "Open report", "Open a report.");

			// Assign the command set to the VUI controller
			_vuiController.AssignCommandSets(commandSet);

			// Open the VUI controller
			_vuiController.Open(this);

			_newReport.IsEnabled = true;
			_openReport.IsEnabled = true;
		}

		private void NewReportButton_Click(object sender, RoutedEventArgs e)
		{
			NewReport();
		}

		private void OpenReport_Click(object sender, RoutedEventArgs e)
		{
			OpenReport();
		}

		// Callback method for application command recognized events
		void vuiController_CommandRecognized(string id, string spokenPhrase, string content, Dictionary<string, string> placeholderValues)
		{
			// Identify the recognized application command first
			if (id == "newreport")
			{
				NewReport();
			}
			if (id == "openreport")
			{
				OpenReport();
			}
		}

		private void NewReport()
		{
			// Show up a new report
			Report report = new Report();
			report.SaveDocument += SaveDocument;
			report.Show();
		}

		private void OpenReport()
		{
			// Open an existing document
			Document document;
			if (_documents.TryGetValue(_documentId.Text, out document))
			{
				// We only show up the report, if a document for the ID already exists.
				Report report = new Report(document);
				report.SaveDocument += SaveDocument;
				report.Show();
			}
		}

		private void Window_Activated(object sender, EventArgs e)
		{
			// We set the focus to the VUI controller whenever the window is activated.
			_vuiController.Focused = true;
		}

		private void SaveDocument(Document document)
		{
			if (_documents.ContainsKey(document.Id))
			{
				_documents[document.Id] = document;
			}
			else
			{
				_documents.Add(document.Id, document);
			}
		}
	}
}
