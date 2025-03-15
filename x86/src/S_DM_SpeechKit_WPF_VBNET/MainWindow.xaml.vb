Class MainWindow

	Private vuiController As Nuance.SpeechAnywhere.WPF.VuiController

	Private Sub OpenSessionBtn_Click(sender As Object, e As RoutedEventArgs) Handles OpenSessionBtn.Click
		REM Disable button - does not make sense to initialize twice
		OpenSessionBtn.IsEnabled = False
		REM Disable field so it is not speech-enabled.  Could also use method VuiController.EnableControl().
		UserNameField.IsEnabled = False
		REM Set the initial focus to the first text box
		HistoryTBox.Focus()



		REM Open the session
		REM TODO: Replace the credentials with the information you received with your Nuance Healthcare Developer registration
		REM Your partner GUID, organization token and serviceUrl will be made available to you via the Nuance order desk Welcome Kit.
		Nuance.SpeechAnywhere.Session.SharedSession.ServiceURL = "ENTER_SERVICE_URL"
		Nuance.SpeechAnywhere.Session.SharedSession.Open(UserNameField.Text, "ENTER_YOUR_ORGANIZATION_TOKEN", "ENTER_YOUR_PARTNER_GUID", "S_DM_SpeechKit_WPF_VBNET")

		REM Create VuiController for this Window
		vuiController = New Nuance.SpeechAnywhere.WPF.VuiController
		REM Set concept names for field navigation
		vuiController.SetConceptName(HistoryTBox, "history")
		vuiController.SetConceptName(FindingsTBox, "findings")
		REM TODO: Additional initializations (e.g. defining application commands) would go here

		REM Initialize VuiController
		vuiController.Open(Me)
	End Sub

End Class
