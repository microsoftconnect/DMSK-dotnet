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

namespace S_SpeechAnywhereWPF
{
	/// <summary>
	/// Interaction logic for Report.xaml
	/// </summary>
	public partial class Report : Window
	{
		private VuiController _vuiController;

		public Report()
		{
			InitializeComponent();
			// The VuiController initialization cannot be done in the constructor,
			// because the controls must be visible for the initialization.
		}

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			InitializeVuiController();
		}

		private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Close the vui controller instance, once it is no longer needed. Releasing the 
			// vui controller frees speech recognition resources on the server and disconnects 
			// from the service. 
			_vuiController.Close();
			// We do not need to take care of recording, as the Dragon Medical SpeechKit will 
			// automatically stop recording for us if it was still running at the time the vui 
			// controller is released. 
		}

		private void RecordButton_Click(object sender, RoutedEventArgs e)
		{
			// Implement a simple toggle recording method. For simplicities sake, we use the current title 
			// of the record toggle button to decide which ISession method to call. 	
			if ((recordButton.Content as string) == "Start recording")
			{
				Session.SharedSession.StartRecording();
			}
			else
			{
				Session.SharedSession.StopRecording();
			}
		}

		private void customizeSpeechBarButton_Click(object sender, RoutedEventArgs e)
		{
			// Override the default speech bar images by custom ones

			// The background of the speech bar. 
			// Minimum size is 324 x 56. Maximum size is this value plus 10% along both axes; i.e. 356 x 62.
			// This is useful for round corner toolbar images.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.SpeechBar, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/toolbar.png", UriKind.Absolute)));

			// The background images of the speech bar's buttons, in their respective states. All button images are 44 x 44.

			// Button background of the record button, when not recording and with no user interaction. A background color value of R=26, G=107, B=150 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOffNormal, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/recbutton_default.png", UriKind.Absolute)));
			// Button background of the record button, when not recording and the mouse hovers over it. A background color value of R=35, G=135, B=190 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOffHover, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/recbutton_default_hover.png", UriKind.Absolute)));
			// Button background of the record button, when not recording and it is being clicked with the mouse. A background color value of R=20, G=85, B=120 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOffPressed, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/recbutton_default_pressed.png", UriKind.Absolute)));

			// Button background of the record button, when recording and with no user interaction. A background color value of R=252, G=1, B=2 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOnNormal, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/recbutton_recording.png", UriKind.Absolute)));
			// Button background of the record button, when recording and the mouse hovers over it. A background color value of R=255, G=98, B=98 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOnHover, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/recbutton_recording_hover.png", UriKind.Absolute)));
			// Button background of the record button, when recording and it is being clicked with the mouse. A background color value of R=193, G=2, B=2 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.RecordButtonOnPressed, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/recbutton_recording_pressed.png", UriKind.Absolute)));

			// Button background of the options ("Nuance flame") button, with no user interaction. A background color value of R=26, G=107, B=150 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.OptionsButtonNormal, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/optionsbutton.png", UriKind.Absolute)));
			// Button background of the record button, when the mouse hovers over it. A background color value of R=35, G=135, B=190 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.OptionsButtonHover, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/optionsbutton_hover.png", UriKind.Absolute)));
			// Button background of the record button, when it is being clicked with the mouse. A background color value of R=20, G=85, B=120 will match that of the inner image of the button.
			SpeechBar.SharedSpeechBar.SetBackgroundImage(Nuance.SpeechAnywhere.BackgroundImageType.OptionsButtonPressed, new BitmapImage(new Uri("pack://application:,,,/S_DM_SpeechKit_WPF;;component/Resources/optionsbutton_pressed.png", UriKind.Absolute)));
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
			_vuiController.Language = XmlLanguage.GetLanguage("en-us");
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

		void SharedSession_RecordingStarted()
		{
			// This event occurs in case recording was started.
			// We react by changing the toggle record button title. 
			recordButton.Content = "Stop recording";
		}

		void SharedSession_RecordingStopped()
		{
			// This event occurs in case recording was stopped.
			// We react by changing the toggle record button title. 
			recordButton.Content = "Start recording";
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
