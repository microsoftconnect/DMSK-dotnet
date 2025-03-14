﻿//
//  LoginDialog.cs
//  SpeechAnywhereSample
//
//  Copyright 2011 Nuance Communications, Inc. All rights reserved.
//

using System;
using System.Windows.Forms;
// Using the Windows Forms API for accessing the Speech Bar functionality. As custom control applications are GUI neutral, the WPF API could also be used.
using Nuance.SpeechAnywhere.WindowsForms;

namespace S_SpeechAnywhere
{
	public partial class LoginDialog : Form
	{
		public string Username
		{
			get { return tbUsername.Text; }
		}
		public LoginDialog()
		{
			InitializeComponent();
			// Set the initial position to 100 pixels away from the upper left corner of the main screen, both in X and Y position
			SpeechBar.SharedSpeechBar.SetInitialPosition(100, 100, true, true);
			// To position the speech bar relative to the bottom and right of the screen, pass "false" for the respective axis.
			// This way, the distance is interpreted between the lower and/or the right edge of the speech bar.
			// Find out the screen size here and position the lower and/or right edge of the speech bar accordingly. 
		}
	}
}
