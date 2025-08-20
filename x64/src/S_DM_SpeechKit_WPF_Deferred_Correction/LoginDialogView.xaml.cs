using Nuance.SpeechAnywhere;
using Nuance.SpeechAnywhere.WPF;
using System.Windows;

namespace S_DM_SpeechKit_WPF_Deferred_Correction
{
	/// <summary>
	/// Interaction logic for LoginDialogView.xaml
	/// </summary>
	public partial class LoginDialogView : Window
	{
		// Your partner GUID, organization token and serviceUrl will be made available to you via the 
		// Nuance order desk Welcome Kit. 
		private const string _partnerGuid = "ENTER_YOUR_PARTNER_GUID";
		private const string _organizationToken = "ENTER_YOUR_ORGANIZATION_TOKEN";
		//By default connect to US production, can override value.
		private const string _serviceUrl = "ENTER_SERVICE_URL";

		public LoginDialogView()
		{
			InitializeComponent();

			TbAuthorId.Text = Settings.DefaultAuthorId;
			TbCorrectionistId.Text = Settings.DefaultCorrectionistId;

			// Set the initial position to 100 logical units away from the upper left corner of the main screen, both in X and Y position.
			// To position the speech bar relative to the bottom and right of the screen, pass "false" for the respective axis.
			// This way, the distance is interpreted between the lower and/or the right edge of the speech bar.
			// Find out the screen size here and position the lower and/or right edge of the speech bar accordingly.
			SpeechBar.SharedSpeechBar.SetInitialPosition(100, 100, true, true);

			if (_serviceUrl != "ENTER_SERVICE_URL")
			{
				Session.SharedSession.ServiceURL = _serviceUrl;
			}
		}

		private void LogonAuthor_Click(object sender, RoutedEventArgs e)
		{
			// We are using region specific url as mentioned above
			Session.SharedSession.ServiceURL = _serviceUrl;
			// Open a new session for the given user. The Session.SharedSession.Close()
			// method should be called when the application or the "document" is closed
			// by the user so that server resources can be freed up immediately.
			Session.SharedSession.Open(TbAuthorId.Text, _organizationToken, _partnerGuid, "S_DM_SpeechKit_WPF_Deferred", DocumentMode.Frontend);

			// For keeping track of the current mode in this sample application.
			Settings.DocumentMode = DocumentMode.Frontend;

			// Your integration should keep track of the currently logged in user.
			Settings.CurrentUser = TbAuthorId.Text;

			Close();
		}

		private void LogonCorrectionist_Click(object sender, RoutedEventArgs e)
		{
			// We are using region specific url as mentioned above
			Session.SharedSession.ServiceURL = _serviceUrl;
			// Open a new session for the given user. The Session.SharedSession.Close()
			// method should be called when the application or the "document" is closed
			// by the user so that server resources can be freed up immediately.
			Session.SharedSession.Open(TbCorrectionistId.Text, _organizationToken, _partnerGuid, "S_DM_SpeechKit_WPF_Deferred", DocumentMode.Correctionist);

			// For keeping track of the current mode in this sample application.
			Settings.DocumentMode = DocumentMode.Correctionist;

			// Your integration should keep track of the currently logged in user.
			Settings.CurrentUser = TbCorrectionistId.Text;

			Close();
		}
	}
}
