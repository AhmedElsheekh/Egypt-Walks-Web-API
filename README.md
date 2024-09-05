# Egypt-Walks-Web-API

**Egypt Walks Web API** is a comprehensive Web API built using .NET Core, designed to provide detailed information about popular walking routes, historical landmarks, and cultural experiences across Egypt. This API allows users to access, manage, and explore walking tour data through RESTful endpoints.

## Table of Contents
- [Project Overview](#project-overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [API Endpoints](#api-endpoints)
- [Data Models](#data-models)
- [Usage](#usage)
- [License](#license)

## Project Overview
Egypt Walks Web API is an extensible API that provides information about walking tours and landmarks across various regions in Egypt. It enables users to search, view, and manage tours, including features such as:
- Listing available walks.
- Viewing detailed information about walks.
- User authentication and authorization.

## Features
- **RESTful API**: Fully REST-compliant API with CRUD operations.
- **Authentication**: Secure user authentication using JWT tokens.
- **Pagination**: Efficiently handles large datasets with pagination.
- **Search and Filter**: Allows users to search for tours.
- **Admin Features**: Allows admins to manage walks, regions and users.
- **Database**: Uses Entity Framework for database management with code-first migrations.

## Technologies Used
- **.NET 8**: Framework for building the API.
- **ASP.NET Core Web API**: For creating the Web API structure.
- **Entity Framework Core**: ORM for managing database operations.
- **SQL Server**: Database system used to store tour, user, and booking data.
- **JWT Authentication**: JSON Web Tokens for user authentication and secure API endpoints.
- **Swagger**: For API documentation and testing.

## Installation

### Prerequisites
- .NET 8 SDK
- SQL Server (local or remote)
- Visual Studio or any preferred code editor

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/egypt-walks-web-api.git
   ```
2. Navigate to the project directory:
   ```bash
   cd egypt-walks-web-api
   ```
3. Set up the database connection in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=yourserver;Database=EgyptWalksDb;Trusted_Connection=True;"
   }
   ```
4. Run database migrations:
   ```bash
   dotnet ef database update
   ```
5. Build and run the application:
   ```bash
   dotnet run
   ```

## API Endpoints

| Method | Endpoint                         | Description                          |
|--------|----------------------------------|--------------------------------------|
| GET    | /api/walks                       | Get a list of all available walks    |
| GET    | /api/walks/{id}                  | Get details of a specific walk       |
| POST   | /api/walks                       | Create a new walk (admin only)       |
| PUT    | /api/walks/{id}                  | Update walk information (admin only) |
| DELETE | /api/walks/{id}                  | Delete a walk (admin only)           |
| POST   | /api/auth/register               | Register a new user                  |
| POST   | /api/auth/login                  | User login and JWT token generation  |

For more details, refer to the automatically generated Swagger documentation available at `/swagger`.

## Data Models

### walk Model
```json
{
  "id": 1,
  "name": "Giza Pyramids Walk",
  "region": "Giza",
  "lengthInKm": "2",
  "description": "Explore the majestic Giza Pyramids on foot."
}
```

### User Model
```json
{
  "id": 1,
  "username": "tourist123",
  "email": "user@example.com",
  "role": "User"
}
```

## Usage
After installation, use Swagger UI (`/swagger`) or a tool like Postman to interact with the API. JWT tokens are required for accessing protected routes (like booking a tour or managing tours).

### Authentication
1. Register or log in using `/api/auth/register` or `/api/auth/login`.
2. Use the generated JWT token in the Authorization header for secured routes.

