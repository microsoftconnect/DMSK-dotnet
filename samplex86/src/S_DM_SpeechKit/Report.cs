//
//  Report.cs
//  Dragon Medical SpeechKit Sample
//
//  Copyright 2011 Nuance Communications, Inc. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nuance.SpeechAnywhere;
using Nuance.SpeechAnywhere.WindowsForms;

namespace S_SpeechAnywhere
{
	public partial class Report : Form
	{
		private VuiController _vuiController;

		public Report()
		{
			InitializeComponent();
			// The VuiController initialization cannot be done in the constructor,
			// because the controls must be visible for the initialization.
		}

		private void InitializeVuiController()
		{
			// Add event handlers for RecordingStarted and RecordingStopped events
			Session.SharedSession.RecordingStarted += new RecordingStarted(SharedSession_RecordingStarted);
			Session.SharedSession.RecordingStopped += new RecordingStopped(SharedSession_RecordingStopped);

			_vuiController = new VuiController();

			// Add event handler for IVuiController event CommandRecognized
			_vuiController.CommandRecognized += new CommandRecognized(vuiController_CommandRecognized);

			// Set the recognition language. This overrides the default setting which is the current UI culture;
			_vuiController.Language = "en-us";
			// Enable name field navigation.
			// For example, the first text control is associated with the "medical history" concept. 
			// Users can say "go to medical history" to reach this text control.
			// SetConceptName() must be called before Initialize() to be effective.
			_vuiController.SetConceptName(textBox1, "medical history");
			_vuiController.SetConceptName(textBox2, "findings");

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

			// Initialize the VuiController by passing the current form as parameter.
			// This will speech enable all supported text controls inside this form and also enable
			// name field navigation as specified above by calling SetConceptName().
			_vuiController.Open(this);
		}

		private void btnRecord_Click(object sender, EventArgs e)
		{
			// Implement a simple toggle recording method. For simplicities sake, we use the current title 
			// of the record toggle button to decide which ISession method to call. 	
			if (btnRecord.Text == "Record")
			{
				Session.SharedSession.StartRecording();
			}
			else
			{
				Session.SharedSession.StopRecording();
			}
		}

		void SharedSession_RecordingStarted()
		{
			// This event occurs in case recording was started.
			// We react by changing the toggle record button title. 
			btnRecord.Text = "Stop";
		}

		void SharedSession_RecordingStopped()
		{
			// This event occurs in case recording was stopped.
			// We react by changing the toggle record button title. 
			btnRecord.Text = "Record";
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
						this.label3.ForeColor = System.Drawing.Color.Red;
					else if (value == "green")
						this.label3.ForeColor = System.Drawing.Color.Green;
					else if (value == "blue")
						this.label3.ForeColor = System.Drawing.Color.Blue;
					else if (value == "black")
						this.label3.ForeColor = System.Drawing.Color.Black;
				}
			}
		}

		private void Report_Load(object sender, EventArgs e)
		{
			InitializeVuiController();
		}

		private void Report_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Close the vui controller instance, once it is no longer needed. Releasing the 
			// vui controller frees speech recognition resources on the server and disconnects 
			// from the service. 
			_vuiController.Close();
			// We do not need to take care of recording, as the Nuance SpeechAnywhere SDK will 
			// automatically stop recording for us if it was still running at the time the vui 
			// controller is released. 
		}
	}
}
