using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace S_DM_SpeechKit_WPF_Deferred_Correction
{
	internal static class DocumentTokenGenerator
	{
		/// <summary>
		///     Result codes from BCrypt APIs
		/// </summary>
		private enum ErrorCode
		{
			Success = 0x00000000,                                       // STATUS_SUCCESS
			AuthenticationTagMismatch = unchecked((int)0xC000A002),     // STATUS_AUTH_TAG_MISMATCH
			BufferToSmall = unchecked((int)0xC0000023),                 // STATUS_BUFFER_TOO_SMALL
		}
		private enum DwFlags
		{
			None = 0x00000000,
			BCryptUseSystemPreferredRng = 0x00000002
		}

		[DllImport("bcrypt.dll")]
		private static extern ErrorCode BCryptGenRandom(int hAlgorithm,
			[In, Out, MarshalAs(UnmanagedType.LPArray)] byte[] pbBuffer,
			int cbBuffer,
			DwFlags dwFlags);

		internal static void GenerateRandomBytes(byte[] randomNumberByteArray)
		{
			var statusCode = BCryptGenRandom(0, randomNumberByteArray, randomNumberByteArray.Length, DwFlags.BCryptUseSystemPreferredRng);
			if (statusCode != ErrorCode.Success)
			{
				throw new CryptographicException($"Error code: {statusCode}");
			}
		}

		internal static string GenerateToken()
		{
			var randomNumberByteArray = new byte[32];
			GenerateRandomBytes(randomNumberByteArray);
			return Convert.ToBase64String(randomNumberByteArray);
		}
	}
}
