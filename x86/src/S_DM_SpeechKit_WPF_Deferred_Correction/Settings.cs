using Nuance.SpeechAnywhere;
using System.Configuration;

namespace S_DM_SpeechKit_WPF_Deferred_Correction
{
	public static class Settings
	{
		/// <summary>
		/// Fields must be loaded with the same field id per document. If you change
		/// this value remember that documents dictated with another field id name will
		/// no longer work correctly when opened for correction.
		/// </summary>
		public static readonly string DocumentFieldId = "Content";

		/// <summary>
		/// The current culture will be used by default if a language is not set on the session.
		/// </summary>
		public static readonly string SrLanguage = ConfigurationManager.AppSettings["SrLanguage"];

		/// <summary>
		/// The default author id.
		/// </summary>
		public static readonly string DefaultAuthorId = ConfigurationManager.AppSettings["DefaultAuthorId"];

		/// <summary>
		/// The default correctionist id.
		/// </summary>
		public static readonly string DefaultCorrectionistId = ConfigurationManager.AppSettings["DefaultCorrectionistId"];

		/// <summary>
		/// The user that is currently logged into the SpeechAnywhere session.
		/// </summary>
		public static string CurrentUser = null;

		/// <summary>
		/// Gets or sets the current document mode. The integrator should keep
		/// track of the currently loaded document mode.
		/// </summary>
		public static DocumentMode DocumentMode { get; set; }

		/// <summary>
		/// Folder to store documents used in this sample application only.
		/// </summary>
		public static readonly string DocumentFolderStore = ConfigurationManager.AppSettings["DocumentFolderStore"];
	}
}
