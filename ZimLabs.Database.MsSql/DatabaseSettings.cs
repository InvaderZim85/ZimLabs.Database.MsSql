using System.Security;

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
        /// Gets or sets the connection timeout (in seconds)
        /// </summary>
        public uint ConnectionTimeout { get; set; }

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
        /// <param name="connectionTimeout">The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error</param>
        public DatabaseSettings(string dataSource, string initialCatalog, string applicationName = "", uint connectionTimeout = 15)
        {
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            IntegratedSecurity = true;
            ApplicationName = applicationName;
            ConnectionTimeout = connectionTimeout;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DatabaseSettings"/>
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="connectionTimeout">The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error</param>
        public DatabaseSettings(string dataSource, string initialCatalog, string userId, string password, string applicationName = "", uint connectionTimeout = 15)
        {
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            UserId = userId;
            Password = password.ToSecureString();
            ApplicationName = applicationName;
            ConnectionTimeout = connectionTimeout;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DatabaseSettings"/>
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        /// <param name="applicationName">The name of the application</param>
        /// <param name="connectionTimeout">The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error</param>
        public DatabaseSettings(string dataSource, string initialCatalog, string userId, SecureString password, string applicationName = "", uint connectionTimeout = 15)
        {
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            UserId = userId;
            Password = password;
            ApplicationName = applicationName;
            ConnectionTimeout = connectionTimeout;
        }
    }
}
