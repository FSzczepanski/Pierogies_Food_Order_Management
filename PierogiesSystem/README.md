## Pierogi System Explenation

# Clean Architecture Solution Template

- Domain
This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

- Application
This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other
layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application
need to access a notification service, a new interface would be added to application and an implementation would be
created within infrastructure.

- Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These
classes should be based on interfaces defined within the application layer.

- WebUI
This layer is a single page application based on Vue 3 and ASP.NET Core 5. This layer depends on both the Application
and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore
only Startup.cs should reference Infrastructure.
<br/>


### Docker Configuration

In order to get Docker working, you will need to add a temporary SSL cert and mount a volume to hold that cert.
You can find [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-3.1) that describe the steps required for Windows, macOS, and Linux.

For Windows:
The following will need to be executed from your terminal to create a cert
`dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Your_password123`
`dotnet dev-certs https --trust`

NOTE: When using PowerShell, replace %USERPROFILE% with $env:USERPROFILE.

FOR macOS:
`dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123`
`dotnet dev-certs https --trust`

FOR Linux:
`dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123`



### Database Migrations


For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\WebUI --output-dir Persistence\Migrations`

update:
`dotnet ef database update --project src\Infrastructure --startup-project src\WebUI 20220114112928_PostionsUpdateMigration`

delete:
`dotnet ef migrations remove --project src\Infrastructure --startup-project src\WebUI`

