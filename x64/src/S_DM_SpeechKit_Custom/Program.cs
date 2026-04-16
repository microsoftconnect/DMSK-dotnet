//
//  Program.cs
//  SpeechAnywhereSample
//
//  Copyright 2011 Nuance Communications, Inc. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Nuance.SpeechAnywhere;

namespace S_SpeechAnywhere
{
	static class Program
	{
		// Your partner GUID, organization token and serviceUrl will be made available to you via the 
		// Nuance order desk Welcome Kit. 
		private const string _partnerGuid = "ENTER_YOUR_PARTNER_GUID";
		private const string _organizationToken = "ENTER_YOUR_ORGANIZATION_TOKEN";

		//By default connect to US production, can override value. 
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

			// Read ServiceUri from app config (injected at test time), fall back to build-time constant
			var configServiceUrl = ConfigurationManager.AppSettings["ServiceUri"];
			if (!string.IsNullOrWhiteSpace(configServiceUrl))
			{
				Session.SharedSession.ServiceURL = configServiceUrl.Trim();
			}
			else if (_serviceUrl != "ENTER_SERVICE_URL" && !string.IsNullOrWhiteSpace(_serviceUrl))
			{
				Session.SharedSession.ServiceURL = _serviceUrl.Trim();
			}
			else
			{
				MessageBox.Show("ServiceURL is not configured. Set ServiceUri in app.config or replace ENTER_SERVICE_URL.",
					"Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
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
