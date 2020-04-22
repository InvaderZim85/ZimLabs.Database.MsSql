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
        public Connector(SecureString connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/> and sets the "IntegratedSecurity" to true
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        public Connector(string dataSource, string initialCatalog) : this(new DatabaseSettings(dataSource, initialCatalog))
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        public Connector(string dataSource, string initialCatalog, string userId, string password) : this(
            new DatabaseSettings(dataSource, initialCatalog, userId, password.ToSecureString()))
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="Connector"/>
        /// </summary>
        /// <param name="dataSource">The data source</param>
        /// <param name="initialCatalog">The initial catalog</param>
        /// <param name="userId">The user id</param>
        /// <param name="password">The password</param>
        public Connector(string dataSource, string initialCatalog, string userId, SecureString password) : this(
            new DatabaseSettings(dataSource, initialCatalog, userId, password))
        {

        }

        /// <summary>
        /// Creates the connection string
        /// </summary>
        private void CreateConnectionString()
        {
            if (_settings.IntegratedSecurity)
            {
                _connectionString = new SqlConnectionStringBuilder
                {
                    DataSource = _settings.DataSource,
                    InitialCatalog = _settings.InitialCatalog,
                    IntegratedSecurity = true,
                    ApplicationName = _settings.ApplicationName
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
                    ApplicationName = _settings.ApplicationName
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

            _connection?.ChangeDatabase(database);
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
