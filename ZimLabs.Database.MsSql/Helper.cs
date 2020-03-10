using System;
using System.Runtime.InteropServices;
using System.Security;

namespace ZimLabs.Database.MsSql
{
    /// <summary>
    /// Provides several helper functions
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// Converts a secure string into a normal string
        /// </summary>
        /// <param name="value">The secure string</param>
        /// <returns>The insecure string</returns>
        /// <exception cref="ArgumentNullException">Will be thrown when the parameter is null</exception>
        public static string ToInsecureString(this SecureString value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        /// <summary>
        /// Converts a string into a secure string
        /// </summary>
        /// <param name="value">The string</param>
        /// <returns>The secure string</returns>
        /// <exception cref="ArgumentNullException">Will be thrown when the parameter is null</exception>
        public static SecureString ToSecureString(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var secureString = new SecureString();

            foreach (var c in value)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }
    }
}
