//
//  Program.cs
//  Dragon Medical SpeechKit Sample
//
//  Copyright 2011 Nuance Communications, Inc. All rights reserved.
//

using System;
using System.Configuration;
using System.Windows.Forms;
using Nuance.SpeechAnywhere;

namespace S_SpeechAnywhere
{
	static class Program
	{
		// Your partner GUID, organization token and serviceUrl will are made available to you via
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
			DialogResult res = loginDialog.ShowDialog();
			loginDialog.Dispose();
			if (res == DialogResult.OK)
			{
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
				// We will use the user name from the login dialog for licensing information passed to the Dragon Medical SpeechKit SDK. 
				Session.SharedSession.Open(loginDialog.txtUserName.Text, _organizationToken, _partnerGuid,
					"S_DM_SpeechKit", DocumentMode.Frontend);

				var createReportWindow = new CreateReport();

				createReportWindow.Closing += (sender, args) =>
				{
					// Close the session. 
					// This disconnects and frees any speech recognition resources
					// and user licenses on the Dragon Medical Server.
					Session.SharedSession.Close();
				};

				Application.Run(createReportWindow);
			}
		}
	}
}
