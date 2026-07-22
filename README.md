# ZEST Student Management System

## Overview

ZEST Student Management System is a full-stack technical assessment project built with an ASP.NET Core Web API backend, a React + Vite frontend, and SQL Server for persistence.

The application provides JWT-based authentication and student management functionality with a layered backend architecture, RESTful APIs, exception handling, logging, API documentation, unit testing, Docker support, and a React user interface.

## Features

- JWT-based login and authentication
- Student CRUD operations
- Get student by ID with 404 handling when the student is not found
- Model validation using ASP.NET Core data annotations
- Global exception handling middleware
- Built-in `ILogger` logging
- Swagger/OpenAPI documentation
- React-based user interface
- Login and logout functionality
- View student list
- Add student
- Edit student
- Delete student with confirmation
- Docker support for the backend API
- Unit testing using xUnit and Moq

## Architecture

The backend follows a layered architecture:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

This separation keeps API handling, business logic, and data-access responsibilities organized independently.

## Technology Stack

### Backend

- .NET 8
- ASP.NET Core Web API
- C#
- Entity Framework Core
- JWT Authentication
- Swagger / OpenAPI
- `ILogger`
- Global Exception Handling Middleware

### Frontend

- React.js
- Vite
- JavaScript
- Axios
- React Router
- CSS

### Database

- Microsoft SQL Server
- Entity Framework Core

### Testing

- xUnit
- Moq

### DevOps

- Docker

## Project Structure

```text
ZEST_Student_Management_System/
│
├── ZEST_Student_Management_System/
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Middleware/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dockerfile
│
├── ZEST_Student_Management_System.Tests/
│   └── Services/
│       └── StudentServiceTests.cs
│
├── zest-student-ui/
│   ├── src/
│   ├── package.json
│   └── .env.example
│
├── .gitignore
└── README.md
```

## Prerequisites

Before running the application, install:

- .NET 8 SDK
- SQL Server or SQL Server Express
- Node.js and npm
- Docker Desktop (optional, for running the backend in Docker)

## Backend Setup

### 1. Clone the repository

```bash
git clone <repository-url>
cd ZEST_Student_Management_System
```

### 2. Configure SQL Server

Configure the `DefaultConnection` connection string according to your local SQL Server environment.

Example using Windows Authentication:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=ZESTStudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

If SQL Server Authentication is used, do not commit real usernames or passwords to source control. Use environment variables or another secure configuration mechanism for sensitive credentials.

### 3. Configure the JWT Signing Key

The JWT signing key is intentionally not stored in `appsettings.json`.

For local development, configure it using .NET User Secrets.

From the backend project directory, use:

```bash
dotnet user-secrets set "Jwt:Key" "<YOUR_STRONG_JWT_SECRET>"
```

Use a sufficiently long and secure secret key.

The non-sensitive JWT settings such as Issuer, Audience, and token expiry are configured in `appsettings.json`.

> Never commit the real JWT signing key to source control.

### 4. Apply Entity Framework Core Migrations

If the database has not already been created, apply the existing migrations:

```bash
dotnet ef database update --project ZEST_Student_Management_System/ZEST_Student_Management_System.csproj
```

Make sure the configured SQL Server instance is running before executing the command.

### 5. Run the Backend

```bash
dotnet run --project ZEST_Student_Management_System/ZEST_Student_Management_System.csproj
```

The backend URLs will be displayed in the console when the application starts.

### 6. Open Swagger

Open Swagger using the HTTPS URL displayed by the backend.

Example:

```text
https://localhost:<backend-https-port>/swagger/index.html
```

Swagger can be used to test authentication and Student APIs.

## Frontend Setup

### 1. Navigate to the React project

```bash
cd zest-student-ui
```

### 2. Install Dependencies

```bash
npm install
```

### 3. Configure the Backend API URL

Create a local `.env` file based on `.env.example`.

For example:

```env
VITE_API_BASE_URL=https://localhost:<backend-https-port>
```

The URL must match the actual URL where the ASP.NET Core backend is running.

When using the Docker configuration documented below, the backend URL can be:

```env
VITE_API_BASE_URL=http://localhost:8090
```

The local `.env` file should not be committed to source control.

### 4. Start the React Application

```bash
npm run dev
```

By default, Vite typically starts the frontend at:

```text
http://localhost:5173
```

Open the displayed URL in your browser.

## Authentication Flow

The authentication flow is:

```text
User Login
    ↓
React sends login credentials
    ↓
ASP.NET Core API validates credentials
    ↓
JWT token is generated
    ↓
React receives and stores the token
    ↓
Axios sends:
Authorization: Bearer <JWT_TOKEN>
    ↓
Protected Student APIs validate the JWT
```

On logout, the frontend removes the JWT token and redirects the user to the login page.

If the API returns `401 Unauthorized`, the frontend removes the invalid or expired token and redirects the user back to login.

> Authentication currently uses fixed demo credentials for assessment purposes. In a production application, users and password hashes should be securely managed using a database or an identity provider.

## API Endpoints

### Authentication

#### Login

```http
POST /api/Auth/login
```

- Public endpoint
- Authenticates the user
- Returns a JWT token when authentication succeeds

### Students

All Student endpoints are protected and require a valid JWT token.

#### Get All Students

```http
GET /api/Student/GetAll
```

Returns all students.

#### Get Student By ID

```http
GET /api/Student/GetById/{id}
```

Returns a student by ID.

Returns `404 Not Found` when the student does not exist.

#### Add Student

```http
POST /api/Student/Add
```

Creates a new student.

#### Update Student

```http
PUT /api/Student/Update/{id}
```

Updates an existing student.

#### Delete Student

```http
DELETE /api/Student/{id}
```

Deletes a student by ID.

### Authorization Header

Protected endpoints require:

```http
Authorization: Bearer <JWT_TOKEN>
```

JWT authentication can also be tested using the **Authorize** option in Swagger.

## Running Unit Tests

The project contains unit tests for `StudentService` using xUnit and Moq.

Run:

```bash
dotnet test ZEST_Student_Management_System.Tests/ZEST_Student_Management_System.Tests.csproj
```

The test suite contains 9 unit tests covering scenarios such as:

- Get all students
- Empty student collection
- Get existing student by ID
- Student not found
- Add student
- Update existing student
- Update non-existing student
- Successful deletion
- Delete non-existing student

The repository layer is mocked using Moq, so unit tests do not require a real SQL Server database.

## Docker

The ASP.NET Core backend can run independently inside a Docker container.

### Build the Docker Image

Run the following command from the repository root:

```powershell
docker build -t zest-student-api:latest -f ZEST_Student_Management_System/Dockerfile .
```

### Run the Docker Container

Sensitive configuration should be passed through environment variables rather than hardcoded into the Dockerfile or committed configuration.

Example PowerShell command:

```powershell
docker run -d --name zest-student-api -p 8090:8080 -e ASPNETCORE_ENVIRONMENT=Development -e "Jwt__Key=<YOUR_JWT_SECRET>" -e "ConnectionStrings__DefaultConnection=<YOUR_DOCKER_SQL_CONNECTION_STRING>" zest-student-api:latest
```

Replace the placeholders with your own local configuration.

Do not commit a command containing real passwords or JWT secrets.

### SQL Server from Docker

If SQL Server is running directly on the Windows host while the ASP.NET Core API is running inside Docker, `localhost` inside the container refers to the container itself.

In that scenario, the SQL Server connection string may need to use:

```text
host.docker.internal
```

instead of:

```text
localhost
```

Example connection string format:

```text
Server=host.docker.internal,1433;Database=ZESTStudentManagementDB;User Id=<USERNAME>;Password=<PASSWORD>;TrustServerCertificate=True;
```

Do not commit real credentials.

### Open Swagger from Docker

After the container starts successfully, Swagger is available at:

```text
http://localhost:8090/swagger/index.html
```

The application root also redirects to Swagger:

```text
http://localhost:8090/
```

## Configuration and Security

The project follows these configuration practices:

- JWT signing keys are not stored in committed configuration.
- Local JWT secrets can be configured using .NET User Secrets.
- Docker secrets and connection settings can be supplied through environment variables.
- React `.env` is excluded from source control.
- `.env.example` provides a safe frontend configuration template.
- Real passwords should never be committed.
- JWT signing secrets should never be committed.
- Connection strings containing sensitive credentials should never be committed.

Example frontend configuration:

```env
VITE_API_BASE_URL=https://localhost:<backend-https-port>
```

No sensitive JWT or SQL credentials should be stored in the React application.

## How to Run the Full Application

### Option 1: Run Backend Locally

1. Start SQL Server.
2. Configure the database connection.
3. Configure `Jwt:Key` using User Secrets.
4. Start the ASP.NET Core backend.
5. Verify the backend using Swagger.
6. Configure `zest-student-ui/.env` with the backend URL.
7. Start the React frontend:

```bash
npm run dev
```

8. Open the React application.
9. Login and use the Student Management functionality.

The application flow is:

```text
React UI
    ↓
CORS
    ↓
ASP.NET Core Web API
    ↓
JWT Authentication
    ↓
Controller
    ↓
Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

### Option 2: Run Backend with Docker

1. Start SQL Server.
2. Build the backend Docker image.
3. Run the container with the required JWT and database environment variables.
4. Verify Swagger at:

```text
http://localhost:8090/swagger/index.html
```

5. Configure the React `.env`:

```env
VITE_API_BASE_URL=http://localhost:8090
```

6. Start the React frontend.

## Assumptions / Notes

- Authentication uses fixed demo credentials for technical assessment purposes.
- In a production system, users and securely hashed passwords should be stored using an appropriate identity solution or database.
- Student validation is handled through DTO/model validation and ASP.NET Core model binding.
- JWT is used to protect Student API endpoints.
- The React frontend stores the JWT in `localStorage` and attaches it to protected API requests.
- SQL Server configuration depends on the developer's local environment.
- Docker Compose is not currently included.

## Future Improvements

Possible future enhancements include:

- Refresh token implementation
- Role-based authorization
- Integration testing
- End-to-end frontend testing
- Docker Compose for API and database orchestration
- Production-grade secret management