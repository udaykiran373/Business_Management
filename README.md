# Business Management API

This project is a Business Management REST API developed using ASP.NET Core and MongoDB. It allows users to create, view, update, and delete business records through a set of REST endpoints.

## Tech Stack

- ASP.NET Core (.NET 10)
- MongoDB
- C#
- Swagger for API testing

## Project Structure

The project follows a layered architecture:

- Controllers – Handles API requests and responses
- Supervisors – Contains business logic
- Repositories – Handles database operations
- Models – Database entities
- DTOs – Request and response models
- Configuration – MongoDB settings

## Features

- Add a new business
- View all businesses
- Get business details by ID
- Update business information
- Soft delete businesses
- MongoDB integration
- Swagger documentation

## Getting Started

### Clone the repository

```bash
git clone <repository-url>
cd BusinessManagement
```

### Configure MongoDB

Update the connection string in `appsettings.json`:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "BusinessManagementDB",
    "BusinessCollectionName": "Businesses"
  }
}
```

### Run the application

```bash
dotnet restore
dotnet run
```

The API will start locally and Swagger will be available in the browser.

## API Endpoints

| Method | Endpoint           | Description          |
| ------ | ------------------ | -------------------- |
| POST   | /api/business      | Create a business    |
| GET    | /api/business      | Get all businesses   |
| GET    | /api/business/{id} | Get business by ID   |
| PUT    | /api/business/{id} | Update business      |
| DELETE | /api/business/{id} | Soft delete business |

## Sample Request

```json
{
  "businessName": "ABC Technologies",
  "creatorId": "user001",
  "creatorName": "Satya"
}
```
