using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ZimLabs.Database.MsSql
{
    /// <summary>
    /// Represents the settings for the database connection
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Gets or sets the name of the server
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// Gets or sets the name of the database
        /// </summary>
        public string InitialCatalog { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public SecureString Password { get; set; }

        /// <summary>
        /// Gets or sets the value which indicates if the integrated security should be used
        /// </summary>
        public bool IntegratedSecurity { get; set; }

        /// <summary>
        /// Gets or sets the name of the application
        /// </summary>
        public string ApplicationName { get; set; }
    }
}
