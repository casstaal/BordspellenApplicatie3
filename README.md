
# Board Game Night Application

A web application developed as part of a Software Engineering assignment at Avans Hogeschool. The application allows users to organize board game nights, discover upcoming events, and register to participate.

The project focuses on building an ASP.NET Core application with user authentication, database management, REST APIs, and GraphQL.

## Features

- **User authentication:** Users can register, log in, and manage their accounts using ASP.NET Core Identity.
- **Board game nights:** Users can create and manage board game nights, including details such as location, date, available games, and maximum number of participants.
- **Event registration:** Users can register for board game nights, with checks to prevent duplicate registrations and exceeding the maximum number of participants.
- **Reviews:** Participants can leave reviews, including a rating and written feedback. Organizers have an average rating based on reviews of their events.
- **REST API:** API endpoints for interacting with the application, including authentication and event registration.
- **GraphQL:** Queries for retrieving board game nights and board games, allowing clients to select the fields they need.

## Technologies

| Category | Technologies |
|---|---|
| Backend | C#, ASP.NET Core MVC, ASP.NET Core Web API |
| Database | Microsoft SQL Server, Entity Framework Core |
| Authentication | ASP.NET Core Identity |
| API | REST, GraphQL (Hot Chocolate) |
| Mapping | AutoMapper |
| Cloud | Microsoft Azure App Service, Azure SQL |
| CI/CD | GitHub Actions |
| Testing | Postman |

## Architecture

The application is structured into separate projects to provide a separation of concerns:

- **Domain:** Contains the domain models and repository interfaces.
- **Infrastructure:** Handles database access through Entity Framework Core and implements the repositories.
- **MVC:** Provides the web interface and handles user interactions.
- **Web API:** Exposes REST endpoints and GraphQL queries for external clients.

### Architectural limitations

Although the project separates domain models, infrastructure, and presentation logic, it does not fully implement Onion Architecture or Clean Architecture.

Currently, there is no dedicated application layer or service layer. Controllers communicate directly with repositories and handle some business logic themselves. As a result, certain controllers have become relatively large and contain responsibilities that would be better handled by separate services.

A future improvement would be to introduce an application layer containing services responsible for business logic, validation, and coordinating repository operations. This would help create smaller controllers, improve separation of concerns, and make the application easier to maintain and test.

The current architecture reflects the scope and time constraints of the school assignment, while providing a foundation for further improvements.

## REST API and GraphQL

The project includes both REST and GraphQL to explore different approaches to API development.

### REST API

REST endpoints are used for operations such as user authentication and registering for board game nights. The API uses HTTP methods and appropriate status codes to communicate the results of requests.

### GraphQL

GraphQL is implemented using Hot Chocolate and provides queries for retrieving board game nights and board games.

Clients can select which fields they want to receive in the response. However, the current implementation retrieves data through Entity Framework Core repositories before GraphQL applies field selection. Therefore, selecting fewer fields does not automatically reduce the amount of data retrieved from the database.

Optimizing database queries based on GraphQL selections would be a possible future improvement.

## Deployment and CI/CD

The application has been deployed to Microsoft Azure using:

- **Azure App Service** for hosting the application.
- **Azure SQL Database** for data storage.
- **GitHub Actions** for automated building and deployment.

The GitHub Actions workflow restores dependencies, builds and publishes the .NET application, and deploys it to Azure.

Database connection strings and other environment-specific configuration should be managed outside the source code.

## Possible Improvements

If I were to continue developing this project, I would focus on:

- Introducing an application layer and service classes to separate business logic from controllers.
- Refactoring larger controllers to improve readability and maintainability.
- Adding more automated unit and integration tests.
- Improving database query efficiency, particularly for GraphQL.
- Further improving error handling and input validation.

## Project Context

This application was developed as an educational project to gain practical experience with ASP.NET Core, database-driven applications, API development, and cloud deployment.

The project is not intended to be a production-ready application. It demonstrates my experience with these technologies, as well as areas where I would improve the architecture and implementation in future development.



## Installation

### Prerequisites

- .NET 8 SDK
- SQL Server or SQL Server Express
- Entity Framework Core CLI (`dotnet-ef`)

### 1. Clone the repository

```powershell
git clone <repository-url>
cd <repository-folder>
```

### 2. Configure the database

Both the MVC application and Web API require a SQL Server connection string.

Configure the `ConnectionStringAzureSQL` setting in both projects using .NET User Secrets or environment variables.

Example connection string:

```text
Server=localhost;Database=BordspellenavondDB;Trusted_Connection=True;TrustServerCertificate=True;
```

### 3. Restore dependencies

```powershell
dotnet restore
```

### 4. Apply database migrations

Apply the existing Entity Framework Core migrations to create the required database tables.

The migrations are located in the `BordspellenApplicatie.Infrastructure` project and use two separate DbContexts:

- `BordspellenavondDBContext` – application data
- `BordspellenavondIdentityDBContext` – authentication and user data

### 5. Run the application

Start the MVC application:

```powershell
dotnet run --project BordspellenApplicatie
```

To run the Web API separately:

```powershell
dotnet run --project Bordspellenapplicatie.WebAPI
```

Open the local URLs displayed in the terminal.

The Web API also provides Swagger (`/swagger`) and GraphQL (`/graphql`) endpoints.

