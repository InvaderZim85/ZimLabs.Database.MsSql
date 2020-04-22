# ZimLabs.Database.MsSql

![Nuget](https://img.shields.io/nuget/v/ZimLabs.Database.MsSql)

This project provides a simple way to create a connection to a MSSQL database.

## Installation

You can install this package via NuGet or download the sources.

```powershell
Install-Package ZimLabs.Database.MsSql
```

## Usage
The usage of the connector is very easy.

The package provides for differen constructors:

```csharp
using ZimLabs.Database.MsSql;

// 1: Constructor with server and database name.
var connector = new Connector("Server", "Database");

// 2: Constructor with server, database, user and password
var connector = new Connector("Server", "Database", "User", "Password");

// 3: Constructor with server, database, user and password (this time as SecureString)
var password = "password".ToSecureString(); 
var connector = new Connector("Server", "Database", "User", password);

// 4: Constructor with settings class
var settings = new DatabaseSettings
{
    DataSource = "127.0.0.1",
    InitialCatalog = "MyFancyDatabase",
    UserId = "Username",
    Password = "Password".ToSecureString(),
    ApplicationName = "MyDummyTool"
};

var connector = new Connector(settings);
```

> **NOTE**: If you use the constructor `Connector(string dataSource, string initialCatalog)` the value for the parameter `IntegratedSecurity` will be set to `true`.

> **NOTE**: The extension method `ToSecureString()` is located in the `Helper` class, which is a part of the package.

## Example
Here a small example (with usage of [Dapper](https://dapper-tutorial.net))

```csharp
// The settings
var settings = new DatabaseSettings
{
    DataSource = "127.0.0.1",
    InitialCatalog = "MyFancyDatabase",
    UserId = "Username",
    Password = "Password".ToSecureString(),
    ApplicationName = "MyDummyTool"
};

var connector = new Connector(settings);

// Perform a query
const string query = "SELECT Id, Name, Mail FROM person AS p";

var personList = connector.Connection.Query<Person>(query).ToList();
```