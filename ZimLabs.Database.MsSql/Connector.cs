using System;
using System.Data;
using System.Data.SqlClient;
using System.Security;

namespace ZimLabs.Database.MsSql
{
    /// <summary>
    /// Provides functions / properties for the interaction with a MySql database
    /// </summary>
    public sealed class Connector : IDisposable
    {
        /// <summary>
        /// Contains the value which indicates if the class was already disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Contains the settings
        /// </summary>
        private readonly DatabaseSettings _settings;

        /// <summary>
        /// Contains the connection string
        /// </summary>
        private SecureString _connectionString;

        /// <summary>
        /// Contains the connection
        /// </summary>
        private SqlConnection _connection;

        /// <summary>
        /// Gets the current connection
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
                    CreateConnection();

                return _connection;
            }
        }

        /// <summary>
        /// Gets the name / path of the current set data source aka server
        /// </summary>
        public string DataSource { get; private set; }

        /// <summary>
        /// Gets the name of the current set database aka initial catalog
        /// </summary>
        public string InitialCatalog { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="settings">The settings for the database connection</param>
        public Connector(DatabaseSettings settings)
        {
            _settings = settings;

            CreateConnectionString();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        [Obsolete("The constructor will be removed in the next version because it is too cumbersome (several steps necessary). Please use one of the other constructors.")]
        public Connector(SecureString connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/> and sets the "IntegratedSecurity" to true
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="applicationName">The name of the application (optional)</param>
        /// <param name="connectionTimeout">The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error</param>
        public Connector(string dataSource, string initialCatalog = "", string applicationName = "", uint connectionTimeout = 15) : this(
            new DatabaseSettings(dataSource, initialCatalog, applicationName, connectionTimeout))
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        /// <param name="applicationName">The name of the application (optional)</param>
        /// <param name="connectionTimeout">The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error</param>
        public Connector(string dataSource, string initialCatalog, string userId, string password, string applicationName = "", uint connectionTimeout = 15) : this(
            new DatabaseSettings(dataSource, initialCatalog, userId, password.ToSecureString(), applicationName, connectionTimeout))
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        /// <param name="applicationName">The name of the application (optional)</param>
        /// <param name="connectionTimeout">The length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error</param>
        public Connector(string dataSource, string initialCatalog, string userId, SecureString password, string applicationName = "", uint connectionTimeout = 15) : this(
            new DatabaseSettings(dataSource, initialCatalog, userId, password, applicationName, connectionTimeout))
        {

        }

        /// <summary>
        /// Creates the connection string
        /// </summary>
        private void CreateConnectionString()
        {
            DataSource = _settings.DataSource;
            InitialCatalog = _settings.InitialCatalog;

            if (_settings.IntegratedSecurity)
            {
                _connectionString = new SqlConnectionStringBuilder
                {
                    DataSource = _settings.DataSource,
                    InitialCatalog = _settings.InitialCatalog,
                    IntegratedSecurity = true,
                    ApplicationName = _settings.ApplicationName,
                    ConnectTimeout = (int) _settings.ConnectionTimeout
                }.ConnectionString.ToSecureString();
            }
            else
            {
                _connectionString = new SqlConnectionStringBuilder
                {
                    DataSource = _settings.DataSource,
                    InitialCatalog = _settings.InitialCatalog,
                    UserID = _settings.UserId,
                    Password = _settings.Password.ToInsecureString(),
                    ApplicationName = _settings.ApplicationName,
                    ConnectTimeout = (int) _settings.ConnectionTimeout
                }.ConnectionString.ToSecureString();
            }
        }

        /// <summary>
        /// Creates a new connection
        /// </summary>
        private void CreateConnection()
        {
            _connection = new SqlConnection(_connectionString.ToInsecureString());
            _connection.Open();
        }

        /// <summary>
        /// Closes the connection
        /// </summary>
        public void CloseConnection()
        {
            _connection?.Close();
        }

        /// <summary>
        /// Switches the database
        /// </summary>
        /// <param name="database">The name of the database</param>
        /// <exception cref="ArgumentNullException">Occurs when the database name is null or empty</exception>
        public void SwitchDatabase(string database)
        {
            if (string.IsNullOrEmpty(database))
                throw new ArgumentNullException(nameof(database));

            InitialCatalog = database;
            _connection?.ChangeDatabase(database);
        }

        /// <summary>
        /// Returns the connection info according to the given types
        /// </summary>
        /// <param name="type">The types</param>
        /// <returns>The connection string info</returns>
        public string ConnectionStringInfo(ConnectionInfoType type)
        {
            return Helper.GetConnectionInfo(type, _settings);
        }

        /// <summary>
        /// Disposes the class
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _connectionString?.Dispose();
            _connection?.Close();
            _connection?.Dispose();

            _disposed = true;
        }
    }
}
