# Mpgp
## Multiplayer Game Platform

### The structure of src folder:

```
.
|-- src/
|   |-- Mpgp.Abstract/
|   |-- Mpgp.DataAccess/
|   |-- Mpgp.Domain/
|   |-- Mpgp.Infrastructure/
|   |-- Mpgp.RestApiServer/
|   |-- Mpgp.Services/
|   |-- Mpgp.Shared/
|   |-- Mpgp.WebSocketServer/
|   L-- Mpgp.sln
```

Abstract - Optional assembly that should not reference any libraries or projects. Contains basic POCO classes used by application (commands, exceptions, DTOs, API requests and responses). This assembly can be shared to other applications.

DataAccess - Assembly that contains database access logic. There should be implementation for Unit of Work, Repositories related to EntityFramework. The assembly references ORM directly.

Domain - Domain of application. Business logic goes here. See below.

Infrastructure - Shared infrastructure details: dependency injection configuration, AutoMapper profile, etc.

RestApiServer - ASP.NET Web API application.

Shared - Helpers, extensions related to .NET go here. For example IEnumerable extensions or validation helpers. The project should not refer other projects.

Services - Services that relates to 3th party dependencies. If you do not want to "pollute" domain with dependencies to other libraries move them to separate assembly. For example that can be IEmailSender in domain and MailKitEmailSender implementation in services.

WebSocketServer - Class library project for a work with the WebSockets.

---

*Project Structure was designed by [Saritasa team](https://github.com/orgs/Saritasa/people).*
