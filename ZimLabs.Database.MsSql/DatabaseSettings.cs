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

        /// <summary>
        /// Creates a new empty instance of the <see cref="DatabaseSettings"/>
        /// </summary>
        public DatabaseSettings(){ }

        /// <summary>
        /// Creates a new instance of the <see cref="DatabaseSettings"/> and sets the <see cref="IntegratedSecurity"/> to true
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="applicationName">The name of the application</param>
        public DatabaseSettings(string dataSource, string initialCatalog, string applicationName = "")
        {
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            IntegratedSecurity = true;
            ApplicationName = applicationName;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DatabaseSettings"/>
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        /// <param name="applicationName">The name of the application</param>
        public DatabaseSettings(string dataSource, string initialCatalog, string userId, SecureString password, string applicationName = "")
        {
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            UserId = userId;
            Password = password;
            ApplicationName = applicationName;
        }
    }
}
