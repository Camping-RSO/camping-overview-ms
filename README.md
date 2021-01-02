# Camping overview ms

A microservice for viewing camps and their info.

Table of Contents
=================
- [Precondition](#precondition)
- [Installation and operation](#installation-and-operation)
- [Environment variables](#environment-variables)
- [Database](#database)


## Precondition

Proper application running tested with the following development tools:
- [Visual Studio 2019](https://visualstudio.microsoft.com/vs/)
- [SSMS 17.x](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)


## Installation and operation

When you have the development environment ready, run the following commands:

1. Open a project in Visual Studio
    - `Open project or solution -> find the downloaded folder and in the subfolder WebApp/ open .sln datoteko`
    - Perform as needed `Restore NuGet Packages`

2. Set up a connection to the database in Visual Studio
    - `WebApp -> appsettings.json -> "ConnectionStrings" -> "DefaultConnection"`
    
3. Start database migration
    - `dotnet ef database update` -> if you use .NET Core CLI
    - `Update-Database -Project WebApp.Infrastructure` -> if you are using the Packege Manager Console
    
4. You can then launch the app


## Environment variables

This microservice camping API requires microservices endpoint. For that you can set `DB_CONNECTION_STRING` environment variable when running with Docker.


## Database

Database image of camping overview ms.

<p align="center">
  <img src="https://raw.githubusercontent.com/camping-rso/camping-overview-ms/master/database/camping-overview-ms.png"/>
</p>
