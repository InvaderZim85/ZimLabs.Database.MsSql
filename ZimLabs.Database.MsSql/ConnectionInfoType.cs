using System;

namespace ZimLabs.Database.MsSql
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
        IntegratedSecurity = 8,

        /// <summary>
        /// Shows the connection timeout
        /// </summary>
        ConnectionTimeout = 16
    }
}
