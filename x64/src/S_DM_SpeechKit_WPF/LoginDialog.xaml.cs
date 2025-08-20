using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nuance.SpeechAnywhere;
using Nuance.SpeechAnywhere.WPF;

namespace S_SpeechAnywhereWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Your partner GUID, organization token and serviceUrl will be made available to you via the 
		// Nuance order desk Welcome Kit.  
		private const string _partnerGuid = "ENTER_YOUR_PARTNER_GUID";
		private const string _organizationToken = "ENTER_YOUR_ORGANIZATION_TOKEN";

		//By default connect to US production, can override value.
		private const string _serviceUrl = "ENTER_SERVICE_URL";

		public MainWindow()
		{
			InitializeComponent();
			// Set the initial position to 100 logical units away from the upper left corner of the main screen, both in X and Y position
			SpeechBar.SharedSpeechBar.SetInitialPosition(100, 100, true, true);
			// To position the speech bar relative to the bottom and right of the screen, pass "false" for the respective axis.
			// This way, the distance is interpreted between the lower and/or the right edge of the speech bar.
			// Find out the screen size here and position the lower and/or right edge of the speech bar accordingly. 
		}

		private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		{
			if (_serviceUrl != "ENTER_SERVICE_URL")
			{
				Session.SharedSession.ServiceURL = _serviceUrl;
			}

			Session.SharedSession.Open(userTextBox.Text, _organizationToken, _partnerGuid, "S_DM_SpeechKit_WPF");
			var report = new Report();

			report.Closing += (sender2, args) =>
			{
				// Close the session. 
				Session.SharedSession.Close();
			};

			report.ShowDialog();
		}
	}
}
