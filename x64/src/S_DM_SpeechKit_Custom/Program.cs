//
//  Program.cs
//  SpeechAnywhereSample
//
//  Copyright 2011 Nuance Communications, Inc. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nuance.SpeechAnywhere;

namespace S_SpeechAnywhere
{
	static class Program
	{
		// Convenience defines used for opening the ISession instance and pass it the licensing 
		// information needed. Your partner GUID, organization token and serviceUrl will be made available to you via the 
		// Nuance order desk Welcome Kit. 
		private const string _partnerGuid = "ENTER_YOUR_PARTNER_GUID";
		private const string _organizationToken = "ENTER_YOUR_ORGANIZATION_TOKEN";
		private const string _serviceUrl = "ENTER_SERVICE_URL";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Use a login dialog to obtain a user name for opening a Dragon Medical Server session
			LoginDialog loginDialog = new LoginDialog();
			if (loginDialog.ShowDialog() != DialogResult.OK)
				return;
			// We are using region specific url as mentioned above
			Session.SharedSession.ServiceURL = _serviceUrl;
			// We will use the user name from the login dialog for licensing information passed to the Dragon Medical SpeechKit SDK. 
			Session.SharedSession.Open(loginDialog.Username, _organizationToken, _partnerGuid, "S_DM_SpeechKit_Custom");

			var mainForm = new Report();

			mainForm.Closing += (sender, args) =>
			{
				// Close the session. 
				Session.SharedSession.Close();
			};

			Application.Run(mainForm);
		}
	}
}
