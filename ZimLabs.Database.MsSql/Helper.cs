using System;
using System.Runtime.InteropServices;
using System.Security;

namespace ZimLabs.Database.MsSql
{
    /// <summary>
    /// Provides several helper functions
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Provides the different connection infos
        /// </summary>
        [Flags]
        public enum ConnectionInfoType
        {
            /// <summary>
            /// Shows nothing
            /// </summary>
            None = 0,

            /// <summary>
            /// Shows the name of the data source (server)
            /// </summary>
            DataSource = 1,

            /// <summary>
            /// Shows the name of the initial catalog (database)
            /// </summary>
            InitialCatalog = 2,

            /// <summary>
            /// Shows the name of the user
            /// </summary>
            User = 4,

            /// <summary>
            /// Shows the integrated security flag
            /// </summary>
            IntegratedSecurity = 8
        }

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
