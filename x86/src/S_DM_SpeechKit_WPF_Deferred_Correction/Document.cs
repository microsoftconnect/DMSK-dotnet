using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace S_DM_SpeechKit_WPF_Deferred_Correction
{
	/// <summary>
	/// Stores a document object. It is up to the integrator to keep track of document
	/// details such as the field content and the corrected state. Documents that are
	/// opened for correction should have the text loaded into the corresponding fields.
	///
	/// These document files are specifically created for this sample application to simplify usage.
	/// Your integration should provide a suitable mechanism to store documents.
	/// </summary>
	[DataContract]
	public class Document
	{
		/// <summary>
		/// The document id. Each document should be given a unique identifier. It is up to the integrator
		/// to keep track of ids for each document.
		/// </summary>
		[DataMember]
		public string Id;

		/// <summary>
		/// The document token. Each document should have a document token. It is up to the integrator
		/// to provide a unique, random and cryptographically secure document token for each document.
		/// </summary>
		[DataMember]
		public string DocumentToken;

		/// <summary>
		/// The content of the document. Documents sent for correction should ideally only have one
		/// editable field associated with the document. The fields must be given the same field id
		/// when reloading an existing document for correction.
		/// </summary>
		[DataMember]
		public string FieldContent;

		/// <summary>
		/// The last known state of the document. Currently this is used for auditing the document.
		/// This state should be kept track of by the integrator.
		/// <see cref="Nuance.SpeechAnywhere.DocumentStates"/> for a list of valid document states.
		/// </summary>
		[DataMember]
		public int DocumentState;
	}

	/// <summary>
	/// Helper methods for managing document files in this sample application.
	///
	/// These document files are specifically created for this sample application to simplify usage.
	/// Your integration should provide a suitable mechanism to store documents.
	/// </summary>
	public static class DocumentExtensions
	{
		public static void SaveToDocumentStore(this Document document, string fileName)
		{
			Directory.CreateDirectory(Settings.DocumentFolderStore);
			Serialize(Path.Combine(Settings.DocumentFolderStore, fileName), document);
		}

		public static void Serialize(string fileName, Document document)
		{
			var serializer = new DataContractJsonSerializer(typeof(Document));

			using (var fs = File.Open(fileName, FileMode.Create))
			{
				serializer.WriteObject(fs, document);
			}
		}

		public static Document Deserialize(string fileName)
		{
			var serializer = new DataContractJsonSerializer(typeof(Document));

			using (var fs = File.OpenRead(fileName))
			{
				return serializer.ReadObject(fs) as Document;
			}
		}
	}
}
