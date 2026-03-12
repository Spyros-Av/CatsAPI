# Cats API

ASP.NET Core Web API for fetching and managing cat images from TheCatAPI with background job processing using Hangfire.

---

## Prerequisites

Before running this application, ensure you have:

1. .NET 8 SDK - Download from: https://dotnet.microsoft.com/download/dotnet/8.0
2. SQL Server (LocalDB, SQL Express, or Full SQL Server)
3. TheCatAPI Key - Sign up for free at: https://thecatapi.com/

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/Spyros-Av/CatsAPI.git
cd CatsAPI
```

### 2. Configure Database Connection

Open the file `CatsAPI/appsettings.json` and update the connection string based on your SQL Server instance:


### 3. Configure TheCatAPI Key

### Note for Reviewers

A TheCatAPI key is included in appsettings.json for demonstration purposes. 
The application is ready to run without additional configuration.

```json
{
  "CatApi": {
    "BaseUrl": "https://api.thecatapi.com/v1/",
    "ApiKey": "live_ci0Bbo42mbh2ZAKNR1lOftnML32P4v0IiTSkXBgHkhSzvCTeLNfSbT4Y2ztHNBOZ"
  }
}
```

Note: Get your free API key by signing up at https://thecatapi.com/signup

### 4. Restore Dependencies

Navigate to the project directory and restore NuGet packages:

```bash
cd CatsAPI
dotnet restore
```

### 5. Set Up the Database

Create the database and tables by running:

```bash
dotnet ef database update
```

This creates the CatsDb database with all required tables.

### 6. Build the Application

```bash
dotnet build
```

You should see: "Build succeeded."

### 7. Run the Application

```bash
dotnet run
```


---

## Using the Application

### Swagger UI (API Testing Interface)

Open your browser and go to: https://localhost:7241/swagger

Here you can test all API endpoints interactively.


---

## API Endpoints

The API provides 4 endpoints:

1. POST /api/cats/fetch - Start a background job to fetch 25 cat images
2. GET /api/cats - Retrieve all cats with pagination and optional tag filtering
3. GET /api/cats/{id} - Retrieve a specific cat by ID
4. GET /api/jobs/{jobId} - Check the status of a background job

---

Built with .NET 8
