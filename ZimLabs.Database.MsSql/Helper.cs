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

        /// <summary>
        /// Converts the settings based on the connection type into info string
        /// </summary>
        /// <param name="type">The info type</param>
        /// <param name="settings">The settings</param>
        /// <returns>The converted info</returns>
        internal static string GetConnectionInfo(ConnectionInfoType type, DatabaseSettings settings)
        {
            var dataSource = $"Data source: {settings.DataSource}";
            var initialCatalog = $"Initial catalog: {settings.InitialCatalog}";
            var user = $"User: {(string.IsNullOrEmpty(settings.UserId) ? "none" : settings.UserId)}";
            var integratedSecurity = $"Integrated security: {(settings.IntegratedSecurity ? "Yes" : "No")}";
            var timeout = $"{settings.ConnectionTimeout}s";

            return (int)type switch
            {
                0 => "",
                1 => dataSource,
                2 => initialCatalog,
                3 => $"{dataSource}; {initialCatalog}",
                4 => user,
                5 => $"{dataSource}; {user}",
                6 => $"{initialCatalog}; {user}",
                7 => $"{dataSource}; {initialCatalog}; {user}",
                8 => integratedSecurity,
                9 => $"{dataSource}; {integratedSecurity}",
                10 => $"{initialCatalog}; {integratedSecurity}",
                11 => $"{dataSource}; {initialCatalog}; {integratedSecurity}",
                12 => $"{user}; {integratedSecurity}",
                13 => $"{dataSource}; {user}; {integratedSecurity}",
                14 => $"{initialCatalog}; {user}; {integratedSecurity}",
                15 => $"{dataSource}; {initialCatalog}; {user}; {integratedSecurity}",
                16 => timeout,
                17 => $"{dataSource}; {timeout}",
                18 => $"{initialCatalog}; {timeout}",
                19 => $"{dataSource}; {initialCatalog}; {timeout}",
                20 => $"{user}; {timeout}",
                21 => $"{dataSource}; {user}; {timeout}",
                22 => $"{initialCatalog}; {user}; {timeout}",
                23 => $"{dataSource}; {initialCatalog}; {user}; {timeout}",
                24 => $"{integratedSecurity}; {timeout}",
                25 => $"{dataSource}; {integratedSecurity}; {timeout}",
                26 => $"{initialCatalog}; {integratedSecurity}; {timeout}",
                27 => $"{dataSource}; {initialCatalog}; {integratedSecurity}; {timeout}",
                28 => $"{user}; {integratedSecurity}; {timeout}",
                29 => $"{dataSource}; {user}; {integratedSecurity}; {timeout}",
                30 => $"{initialCatalog}; {user}; {integratedSecurity}; {timeout}",
                31 => $"{dataSource}; {initialCatalog}; {user}; {integratedSecurity}; {timeout}",
                32 => "",
                _ => ""
            };
        }
    }
}
