# WebApi_TodoApiSample - EF Core Example

## Overview

The **WebApi_TodoApiSample** project is a simple API built using **ASP.NET Core** and **Entity Framework Core** to demonstrate various CRUD operations, including **DELETE**, **POST**, **PUT**, and **PATCH** with EF Core.

This API allows users to manage a collection of todo items with different methods of interaction.

## Features

- **EF Core Operations**: Demonstrates the usage of Entity Framework Core to handle database operations.
- **RESTful API**: Exposes various endpoints for managing todo items.
- **Data Validation**: Includes EF Core model validation for data integrity.

## Project Structure

The project is organized as follows:

```plaintext
/Name
|-- WebApi_TodoApiSample
|-- Controllers
|   |-- TodoController.cs
|-- Entities
|   |-- Todo.cs
|-- Migrations
|   |-- InitialCreate.cs
|-- Models
|   |-- TodoModel.cs
|-- Properties
|   |-- launchSettings.json
|-- Program.cs
|-- Startup.cs
|-- WebApi_TodoApiSample.csproj
|-- appsettings.Development.json
|-- appsettings.json
```

### Key Files

- **Controllers**: Contains the API controllers, such as `TodoController.cs`, which handles incoming HTTP requests.
- **Entities**: Defines the database entities like `Todo.cs` used by EF Core for database operations.
- **Migrations**: Holds migration files to manage database schema changes.
- **Models**: Defines the data models used for interaction between the API and the client, such as `TodoModel.cs`.
- **Program.cs**: Configures and runs the application.
- **Startup.cs**: Configures services like authentication, routing, and other application-level configurations.
- **appsettings.json**: Holds configuration settings for the application, such as database connection strings.

## Setup Instructions

### Prerequisites

- .NET Core SDK 3.1 or later
- A database (e.g., SQL Server, SQLite) configured for EF Core

### Clone the Repository

To get started, clone the repository to your local machine:

```bash
git clone <repository_url>
cd WebApi_TodoApiSample
```

### Restore Dependencies

Run the following command to restore the project dependencies:

```bash
dotnet restore
```

### Apply Migrations

Apply the EF Core migrations to set up the database schema:

```bash
dotnet ef database update
```

### Run the Application

To run the application, use the following command:

```bash
dotnet run
```

The application will be hosted at [http://localhost:5000](http://localhost:5000) by default.

## API Endpoints

The following API endpoints are available in the application:

- **POST /api/todos**: Create a new todo item.
- **GET /api/todos**: Retrieve a list of all todo items.
- **GET /api/todos/{id}**: Retrieve a specific todo item by its ID.
- **PUT /api/todos/{id}**: Update an existing todo item.
- **PATCH /api/todos/{id}**: Partially update a todo item.
- **DELETE /api/todos/{id}**: Delete a specific todo item.

## Authentication

The API uses JWT (JSON Web Tokens) to secure access to some of the endpoints. To interact with the protected routes, a valid token must be included in the `Authorization` header of the request.

Example header format:

```plaintext
Authorization: Bearer <your_token>
```

## Project Development

If you'd like to contribute to the project, follow these steps:

1. Fork the repository.
2. Clone your fork locally.
3. Create a new branch for your changes.
4. Commit your changes with meaningful messages.
5. Push your changes to your fork.
6. Submit a pull request to the original repository.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

## Acknowledgements

This API was created using **ASP.NET Core** and **Entity Framework Core** for demonstrating common CRUD operations.  
A special thank you to the open-source community for providing the tools and libraries used in this project.

For more information on ASP.NET Core and Entity Framework Core, please refer to the official documentation.

---
