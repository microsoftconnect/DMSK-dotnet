using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Nuance.SpeechAnywhere;
using Nuance.SpeechAnywhere.WPF;

namespace S_SpeechAnywhereDocApi
{
	/// <summary>
	/// Class holding document data
	/// </summary>
	public class Document
	{
		public string Id { get; set; }
		public string MedicalHistory { get; set; }
		public string Findings { get; set; }
	}

	/// <summary>
	/// Interaction logic for Report.xaml
	/// </summary>
	public partial class Report : Window
	{

		private VuiController _vuiController;

		// This is the default document state.
		private int _documentState = DocumentStates.Undefined;

		public Report()
		{
			InitializeComponent();
			// Opening the VUI controller cannot be done in the constructor,
			// because the controls must be visible when the VUI controller is opened.
		}

		public Report(Document document)
		{
			InitializeComponent();
			// Opening the VUI controller cannot be done in the constructor,
			// because the controls must be visible when the VUI controller is opened.

			// Set the corresponding document ID and fill the report with the document data.
			_documentId.Text = document.Id;
			_medicalHistory.Text = document.MedicalHistory;
			_findings.Text = document.Findings;
		}

		public delegate void SaveDocumentHandler(Document document);
		public event SaveDocumentHandler SaveDocument;

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			OpenVuiController();
		}

		private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Close the VUI controller instance, once it is no longer needed. Releasing the 
			// VUI controller frees speech recognition resources on the server and disconnects 
			// from the service. 
			// We use two options to Close the VUI controller.
			if (string.IsNullOrEmpty(_documentId.Text))
			{
				// 1) Close the VUI controller and save the content of the document under the name that was specified at opening
				_vuiController.Close();
			}
			else
			{
				// 2) Close the VUI controller and pass the document state
				// In this implementation we use two of the predefined document states:
				// Default it is Undefined, or in case the report has been signed it is Corrected.
				_vuiController.Close(_documentId.Text, _documentState);

				// Save the document under the given ID, so that it can be continued to work on at a later point in time.
				if (SaveDocument != null)
				{
					Document document = new Document();
					document.Id = _documentId.Text;
					document.MedicalHistory = _medicalHistory.Text;
					document.Findings = _findings.Text;
					SaveDocument(document);
				}
			}
		}

		private void SignAndCloseButton_Click(object sender, RoutedEventArgs e)
		{
			// Sign report -> set document state to Corrected. At the point where a provider signs a report it is quite probable that they
			// also reviewed the text and corrected misrecognitions, therefore this state is applicable at this point in the workflow.
			_documentState = DocumentStates.Corrected;
			Close();
		}

		private void ActivateButton_Click(object sender, RoutedEventArgs e)
		{
			_vuiController.Focused = true;
		}

		private void OpenVuiController()
		{
			_vuiController = new VuiController();

			// On the VUI controller, set the AutoFocus property to false. This enables explicit control over the focus via the Focused property and 
			// disables the legacy behavior of implicitly activating the VUI controller instance whenever Initialize(), Open() or Synchronize() is called.
			_vuiController.AutoFocus = false;

			// Add event handler for VUI controller event CommandRecognized
			_vuiController.CommandRecognized += new CommandRecognized(vuiController_CommandRecognized);

			// Set the recognition language. This overrides the default setting which is the current UI culture;
			_vuiController.Language = XmlLanguage.GetLanguage("en-us");
			// Enable name field navigation.
			// For example, the first text control is associated with the "medical history" concept. 
			// Users can say "go to medical history" to reach this text control.
			// SetConceptName() must be called before Initialize() to be effective.
			_vuiController.SetConceptName(_medicalHistory, "medical history");
			_vuiController.SetConceptName(_findings, "findings");

			// Set document field ids
			_vuiController.SetDocumentFieldId(_medicalHistory, "medHistoryField");
			_vuiController.SetDocumentFieldId(_findings, "findingsField");

			// Application command and command placeholder setup

			// Create a single element array for the command set
			CommandSet[] commandSet = new CommandSet[1];
			// Create command set with ID and description
			commandSet[0] = new CommandSet("ColorCommandSet", "Commands for changing sample text color");
			// Create the "change color" application command (id: "setcolor") with two spoken forms: "set color to ..." and "change color to ...".
			// The corresponding written forms are also supplied. The description of the command is only given in the first call.
			commandSet[0].CreateCommand("setcolor", "set color to <color>", "Set color to <color>", "Set the sample text color to the given color.");
			commandSet[0].CreateCommand("setcolor", "change color to <color>", "Change color to <color>", "");

			// Create a single element array for the command placeholder
			CommandPlaceholder[] commandPlaceholder = new CommandPlaceholder[1];
			// Create the command placeholder object (id: "color", label: "Color placeholder")
			commandPlaceholder[0] = new CommandPlaceholder("color", "Color placeholder");
			// Set the command placeholder values and spoken forms
			commandPlaceholder[0].SetValues(new string[] { "red", "green", "blue", "black" }, new string[] { "red", "green", "blue", "black" });

			// Assign the command set to the VUI controller
			_vuiController.AssignCommandSets(commandSet);
			// Assign the command placeholder to the VUI controller
			_vuiController.AssignCommandPlaceholders(commandPlaceholder);

			// Open the VUI controller by passing the current form as parameter.
			// This will speech enable all supported text controls inside this form and also enable
			// name field navigation as specified above by calling SetConceptName().
			if (string.IsNullOrEmpty(_documentId.Text))
			{
				// Open the VUI controller with an unnamed document
				_vuiController.Open(this);
			}
			else
			{
				// Open the VUI controller with the specified document
				_vuiController.Open(this, _documentId.Text);
			}
		}

		// Callback method for application command recognized events
		void vuiController_CommandRecognized(string id, string spokenPhrase, string content, Dictionary<string, string> placeholderValues)
		{
			// Identify the recognized application command first
			if (id == "setcolor")
			{
				// Identify the recognized placeholder and its value, and perform the appropriate action
				// (change the sample text color)
				if (placeholderValues.ContainsKey("color"))
				{
					string value = placeholderValues["color"];
					if (value == "red")
						this.textBlockSample.Foreground = new SolidColorBrush(Colors.Red);
					else if (value == "green")
						this.textBlockSample.Foreground = new SolidColorBrush(Colors.Green);
					else if (value == "blue")
						this.textBlockSample.Foreground = new SolidColorBrush(Colors.Blue);
					else if (value == "black")
						this.textBlockSample.Foreground = new SolidColorBrush(Colors.Black);
				}
			}
		}
	}
}
