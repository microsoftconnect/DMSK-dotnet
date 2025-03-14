//
//  Report.cs
//  SpeechAnywhereSample
//
//  Copyright 2011 Nuance Communications, Inc. All rights reserved.
//

using System;
using System.Windows.Forms;
using Nuance.SpeechAnywhere;
using Nuance.SpeechAnywhere.Custom;
using Nuance.SpeechAnywhere.WindowsForms;

namespace S_SpeechAnywhere
{
	public partial class Report : Form
	{
		private MyTextControl[] myControls;
		private Nuance.SpeechAnywhere.Custom.VuiController vuiController;

		public Report()
		{
			InitializeComponent();

			// my "custom constrols" are simple wrappers for the textboxes.
			myControls = new MyTextControl[] { new MyTextControl(tbMedicalHistory), new MyTextControl(tbFindings) };

			InitializeVuiController();
		}
		private void InitializeVuiController()
		{
			// Add event handlers for RecordingStarted and RecordingStopped events
			Session.SharedSession.RecordingStarted += new RecordingStarted(SharedSession_RecordingStarted);
			Session.SharedSession.RecordingStopped += new RecordingStopped(SharedSession_RecordingStopped);

			vuiController = new Nuance.SpeechAnywhere.Custom.VuiController();

			// Set the recognition language. This overrides the default setting which is the current UI culture;
			vuiController.Language = "en-us";
			// Enable name field navigation.
			// For example, the first text control is associated with the "medical history" concept. 
			// Users can say "go to medical history" to reach this text control.
			// SetConceptName() must be called before Initialize() to be effective.
			vuiController.SetConceptName(myControls[0], "medical history");
			vuiController.SetConceptName(myControls[1], "findings");

			// Initialize the VuiController by passing Nuance.SpeechAnywhere.Custom.ITextControl[] - which is in this case "myControls"
			vuiController.Open(myControls);
			vuiController.Focused = true;
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

		private void btClose_Click(object sender, EventArgs e)
		{
			Close();
		}
		private void Report_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Close the vui controller instance, once it is no longer needed. Releasing the 
			// vui controller frees speech recognition resources on the server and disconnects 
			// from the service. 
			vuiController.Close();
			// We do not need to take care of recording, as the Dragon Medical SpeechKit will 
			// automatically stop recording for us if it was still running at the time the vui 
			// controller is released. 
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

		private void buttonCustomizeSpeechBar_Click(object sender, EventArgs e)
		{
			// Override the default speech bar images by custom ones

			// The background of the speech bar. 
			// Minimum size is 324 x 56 pixels. Maximum size is this value plus 10% along both axes; i.e. 356 x 61 pixels.
			// This is useful for round corner toolbar images.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.SpeechBar, Properties.Resources.toolbar);

			// The background images of the speech bar's buttons, in their respective states. All button images are 44 x 44 pixels.

			// Button background of the record button, when not recording and with no user interaction. A background color value of R=26, G=107, B=150 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOffNormal, Properties.Resources.recbutton_default);
			// Button background of the record button, when not recording and the mouse hovers over it. A background color value of R=35, G=135, B=190 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOffHover, Properties.Resources.recbutton_default_hover);
			// Button background of the record button, when not recording and it is being clicked with the mouse. A background color value of R=20, G=85, B=120 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOffPressed, Properties.Resources.recbutton_default_pressed);

			// Button background of the record button, when recording and with no user interaction. A background color value of R=252, G=1, B=2 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOnNormal, Properties.Resources.recbutton_recording);
			// Button background of the record button, when recording and the mouse hovers over it. A background color value of R=255, G=98, B=98 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOnHover, Properties.Resources.recbutton_recording_hover);
			// Button background of the record button, when recording and it is being clicked with the mouse. A background color value of R=193, G=2, B=2 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOnPressed, Properties.Resources.recbutton_recording_pressed);

			// Button background of the options ("Nuance flame") button, with no user interaction. A background color value of R=26, G=107, B=150 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.OptionsButtonNormal, Properties.Resources.optionsbutton);
			// Button background of the record button, when the mouse hovers over it. A background color value of R=35, G=135, B=190 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.OptionsButtonHover, Properties.Resources.optionsbutton_hover);
			// Button background of the record button, when it is being clicked with the mouse. A background color value of R=20, G=85, B=120 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.OptionsButtonPressed, Properties.Resources.optionsbutton_pressed);
		}
	}

	public class MyTextControl : Nuance.SpeechAnywhere.Custom.ITextControl
	{
		TextBox myCustomTextBox;
		public MyTextControl(TextBox box)
		{
			myCustomTextBox = box;
			box.GotFocus += box_GotFocus;
		}

		void box_GotFocus(object sender, EventArgs e)
		{
			if (GotFocus != null)
			{
				GotFocus(this);
			}
		}

		#region ITextControl Members

		public event Action<object> GotFocus;

		public bool Focused
		{
			get
			{
				return myCustomTextBox.Focused;
			}
			set
			{
				myCustomTextBox.Focus();
			}
		}

		public int TextLength
		{
			get { return myCustomTextBox.TextLength; }
		}

		public string NewlineFormatString
		{
			get { return Environment.NewLine; }
		}

		public string ParagraphFormatString
		{
			get { return Environment.NewLine + Environment.NewLine; }
		}

		public void SetSelection(int start, int length)
		{
			myCustomTextBox.SelectionStart = start;
			myCustomTextBox.SelectionLength = length;
		}

		public void GetSelection(ref int start, ref int length)
		{
			start = myCustomTextBox.SelectionStart;
			length = myCustomTextBox.SelectionLength;
		}

		public string GetText(int start, int length)
		{
			return myCustomTextBox.Text;
		}

		public void ReplaceText(int start, int length, string newText)
		{
			SetSelection(start, length);
			myCustomTextBox.SelectedText = newText;
		}

		#endregion
	}
}
