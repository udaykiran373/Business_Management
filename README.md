# Business Management API

A simple **Business Management Module** built with:
- **C#** / **ASP.NET Core 8**
- **MongoDB** (via official MongoDB.Driver)
- **Honeycomb Architecture** (Controller → Supervisor → Repository → MongoDB)

---

## Architecture Overview

```
HTTP Request
     ↓
┌─────────────────┐
│   Controller    │  ← Handles HTTP requests & responses
└────────┬────────┘
         ↓
┌─────────────────┐
│   Supervisor    │  ← Business logic, validation, data processing
└────────┬────────┘
         ↓
┌─────────────────┐
│   Repository    │  ← MongoDB CRUD operations
└────────┬────────┘
         ↓
┌─────────────────┐
│    MongoDB      │  ← Data persistence
└─────────────────┘
```

---

## Prerequisites

| Tool | Version | Download |
|------|---------|----------|
| .NET SDK | 10.0+ | https://dotnet.microsoft.com/download |
| MongoDB | 6.0+ | https://www.mongodb.com/try/download/community |

---

## Project Structure

```
BusinessManagement/
├── Controllers/
│   └── BusinessController.cs      # API endpoints
├── Supervisors/
│   ├── IBusinessSupervisor.cs     # Supervisor interface
│   └── BusinessSupervisor.cs      # Business logic & validation
├── Repositories/
│   ├── IBusinessRepository.cs     # Repository interface
│   └── BusinessRepository.cs     # MongoDB operations
├── Models/
│   └── Business.cs                # MongoDB document model
├── DTOs/
│   └── BusinessDTOs.cs            # Request & response DTOs
├── Configuration/
│   └── MongoDbSettings.cs         # MongoDB config model
├── Program.cs                     # App entry point & DI setup
├── appsettings.json               # App configuration
└── BusinessManagement.csproj      # Project file
```

---

## Setup & Run Instructions

### Step 1 – Start MongoDB

Make sure MongoDB is running locally on the default port `27017`.

**Windows (if installed as a service):**
```bash
net start MongoDB
```

**macOS (with Homebrew):**
```bash
brew services start mongodb-community
```

**Linux:**
```bash
sudo systemctl start mongod
```

**Using Docker (alternative):**
```bash
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

---

### Step 2 – Configure MongoDB Connection

Open `appsettings.json` and update if needed:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "BusinessManagementDB",
    "BusinessCollectionName": "Businesses"
  }
}
```

> The database and collection are **created automatically** by MongoDB on first insert — no manual setup required.

---

### Step 3 – Restore & Run

```bash
# Navigate to the project folder
cd BusinessManagement

# Restore NuGet packages
dotnet restore

# Run the application
dotnet run
```

The API will start at:
- **HTTP:**  `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`

---

### Step 4 – Open Swagger UI

Navigate to: **`http://localhost:5000`**

Swagger UI loads at the root and lets you test all APIs interactively.

---

## API Reference

| # | API Name | Method | Endpoint |
|---|----------|--------|----------|
| 1 | Add Business | `POST` | `/api/business` |
| 2 | Get All Businesses | `GET` | `/api/business` |
| 3 | Get Business By Id | `GET` | `/api/business/{businessId}` |
| 4 | Edit Business | `PUT` | `/api/business/{businessId}` |
| 5 | Delete Business | `DELETE` | `/api/business/{businessId}` |

---

### 1. Add Business — `POST /api/business`

**Request Body:**
```json
{
  "businessName": "Tech Solutions Pvt Ltd",
  "creatorId": "user-001",
  "creatorName": "Arjun Sharma"
}
```

**Success Response `200`:**
```json
{
  "success": true,
  "message": "Business added successfully.",
  "data": {
    "businessId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "businessName": "Tech Solutions Pvt Ltd",
    "creatorId": "user-001",
    "creatorName": "Arjun Sharma",
    "createdDate": "2025-01-01T10:00:00Z",
    "updatedDate": "2025-01-01T10:00:00Z",
    "isDeleted": false
  }
}
```

---

### 2. Get All Businesses — `GET /api/business`

**Success Response `200`:**
```json
{
  "success": true,
  "message": "2 business(es) retrieved successfully.",
  "data": [ { ... }, { ... } ]
}
```

---

### 3. Get Business By Id — `GET /api/business/{businessId}`

**Success Response `200`:**
```json
{
  "success": true,
  "message": "Business retrieved successfully.",
  "data": { ... }
}
```

**Not Found Response `404`:**
```json
{
  "success": false,
  "message": "Business not found.",
  "data": null
}
```

---

### 4. Edit Business — `PUT /api/business/{businessId}`

**Request Body:**
```json
{
  "businessName": "Tech Solutions Global Ltd",
  "creatorId": "user-001",
  "creatorName": "Arjun Sharma"
}
```

**Success Response `200`:**
```json
{
  "success": true,
  "message": "Business updated successfully.",
  "data": { ... }
}
```

---

### 5. Delete Business — `DELETE /api/business/{businessId}`

> Performs a **soft delete** — sets `isDeleted: true` instead of removing the record.

**Success Response `200`:**
```json
{
  "success": true,
  "message": "Business deleted successfully.",
  "data": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

---

## MongoDB Document Structure

```json
{
  "_id": "<ObjectId>",
  "businessId": "<GUID>",
  "businessName": "Tech Solutions Pvt Ltd",
  "creatorId": "user-001",
  "creatorName": "Arjun Sharma",
  "createdDate": "2025-01-01T10:00:00Z",
  "updatedDate": "2025-01-01T10:00:00Z",
  "isDeleted": false
}
```

---

## Notes

- **`businessId`** is a system-generated GUID (not MongoDB's `_id`)
- **Soft Delete** — deleted businesses are hidden from all GET results but remain in the DB
- All timestamps are stored in **UTC**
- All APIs return a consistent `ApiResponse<T>` wrapper with `success`, `message`, and `data`
