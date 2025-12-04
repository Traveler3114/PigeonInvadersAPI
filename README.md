# Pigeon Invaders API

A RESTful API backend for the Pigeon Invaders game, built with ASP.NET Core and MySQL. This API manages player scores, timers, and leaderboard data.

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [API Usage Examples](#api-usage-examples)
- [Development](#development)
- [Swagger Documentation](#swagger-documentation)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)

## 🎮 Overview

Pigeon Invaders API is a backend service designed to support the Pigeon Invaders game. It provides endpoints for storing and retrieving player performance data, including usernames, scores, and completion times. The API uses MySQL for persistent data storage and includes built-in Swagger documentation for easy testing and integration.

## ✨ Features

- **Player Score Management**: Add new player scores with username, score, and timer data
- **Leaderboard Retrieval**: Fetch all player records from the database
- **Timer Validation**: Automatic validation and formatting of time values (HH:mm:ss format)
- **RESTful Design**: Clean, intuitive API endpoints following REST principles
- **Swagger Integration**: Interactive API documentation and testing interface
- **CORS Ready**: Configured for cross-origin requests
- **Error Handling**: Comprehensive error messages for debugging

## 🛠 Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Language**: C# with .NET 8.0
- **Database**: MySQL
- **ORM/Database Access**: MySql.Data (ADO.NET)
- **API Documentation**: Swashbuckle.AspNetCore (Swagger/OpenAPI)
- **Architecture**: MVC pattern with Controllers

### Key Dependencies

- `MySql.Data` (v9.1.0) - MySQL database connectivity
- `Swashbuckle.AspNetCore` (v6.6.2) - Swagger/OpenAPI documentation

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) (version 8.0 or later recommended)
- A code editor (Visual Studio, Visual Studio Code, or JetBrains Rider recommended)
- MySQL Workbench or another MySQL client (optional, for database management)

## 🚀 Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Traveler3114/PigeonInvadersAPI.git
   cd PigeonInvadersAPI
   ```

2. **Restore NuGet packages**:
   ```bash
   dotnet restore
   ```

3. **Build the project**:
   ```bash
   dotnet build
   ```

## 🗄 Database Setup

1. **Create the MySQL database**:
   ```sql
   CREATE DATABASE PigeonInvadersDB;
   ```

2. **Create the Players table**:
   ```sql
   USE PigeonInvadersDB;

   CREATE TABLE Players (
       id INT AUTO_INCREMENT PRIMARY KEY,
       username VARCHAR(255) NOT NULL,
       score INT NOT NULL,
       Timer TIME NOT NULL,
       created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
   );
   ```

3. **Verify the table structure**:
   ```sql
   DESCRIBE Players;
   ```

## ⚙️ Configuration

### Database Connection

Update the connection string in `MySqlConnectionManager.cs` with your MySQL credentials:

```csharp
private string connectionString = "Server=localhost;Database=PigeonInvadersDB;User ID=your_username;Password=your_password;";
```

**Security Note**: ⚠️ **CRITICAL**: The connection string in the repository currently contains real database credentials that are exposed in version control. This is a serious security vulnerability. You must immediately:
- Change your database password
- Remove hardcoded credentials from the code
- Use environment variables or configuration files (excluded from git)

For secure credential management, consider:
- Using environment variables
- Storing credentials in `appsettings.json` (excluded from version control)
- Using Azure Key Vault or similar secret management solutions
- Implementing user secrets for development

### App Settings

The application uses standard ASP.NET Core configuration files:

- `appsettings.json` - Production settings
- `appsettings.Development.json` - Development-specific settings

## ▶️ Running the Application

### Development Mode

Run the application in development mode with hot reload:

```bash
dotnet run
```

Or using the watch command for automatic recompilation:

```bash
dotnet watch run
```

The API will start on:
- HTTPS: `https://localhost:7189`
- HTTP: `http://localhost:5181`

### Production Mode

Build and run in production:

```bash
dotnet build --configuration Release
dotnet run --configuration Release
```

## 📡 API Endpoints

### Base URL
- Development: `http://localhost:5181/api`
- Production: `https://your-domain.com/api`

### Endpoints

#### 1. Add Player Score

**POST** `/api/Values/AddPlayer`

Adds a new player record with username, score, and timer.

**Request Body**:
```json
{
  "username": "Player1",
  "score": 1500,
  "timer": "00:05:30"
}
```

**Response** (Success - 200 OK):
```json
"Score added successfully!"
```

**Response** (Error - 400 Bad Request):
```json
"Error: [error message]"
```

**Validation Rules**:
- `username`: Required, string
- `score`: Required, integer
- `timer`: Required, format must be "HH:mm:ss" (e.g., "00:05:30" for 5 minutes 30 seconds)

---

#### 2. Get All Players

**GET** `/api/Values/GetPlayers`

Retrieves all player records from the database.

**Response** (Success - 200 OK):
```json
[
  {
    "username": "Player1",
    "score": 1500,
    "timer": "00:05:30"
  },
  {
    "username": "Player2",
    "score": 2000,
    "timer": "00:04:15"
  }
]
```

**Response** (Error - 400 Bad Request):
```json
"Error: [error message]"
```

## 💻 API Usage Examples

### Using cURL

**Add a Player**:
```bash
curl -X POST "http://localhost:5181/api/Values/AddPlayer" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "Player1",
    "score": 1500,
    "timer": "00:05:30"
  }'
```

**Get All Players**:
```bash
curl -X GET "http://localhost:5181/api/Values/GetPlayers" \
  -H "accept: application/json"
```

### Using PowerShell

**Add a Player**:
```powershell
$body = @{
    username = "Player1"
    score = 1500
    timer = "00:05:30"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5181/api/Values/AddPlayer" `
  -Method Post `
  -ContentType "application/json" `
  -Body $body
```

**Get All Players**:
```powershell
Invoke-RestMethod -Uri "http://localhost:5181/api/Values/GetPlayers" `
  -Method Get
```

### Using JavaScript (Fetch API)

**Add a Player**:
```javascript
const addPlayer = async () => {
  const response = await fetch('http://localhost:5181/api/Values/AddPlayer', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      username: 'Player1',
      score: 1500,
      timer: '00:05:30'
    })
  });
  
  const data = await response.json();
  console.log(data);
};
```

**Get All Players**:
```javascript
const getPlayers = async () => {
  const response = await fetch('http://localhost:5181/api/Values/GetPlayers');
  const players = await response.json();
  console.log(players);
};
```

### Using C# HttpClient

```csharp
using System.Net.Http.Json;

// Add a Player
var player = new Player
{
    username = "Player1",
    Score = 1500,
    Timer = "00:05:30"
};

var response = await httpClient.PostAsJsonAsync(
    "http://localhost:5181/api/Values/AddPlayer", 
    player
);

// Get All Players
var players = await httpClient.GetFromJsonAsync<List<Player>>(
    "http://localhost:5181/api/Values/GetPlayers"
);
```

## 🔧 Development

### Project Structure

```
PigeonInvadersAPI/
├── Controllers/
│   └── ValuesController.cs       # API endpoints controller
├── Properties/
│   └── launchSettings.json       # Launch configuration
├── appsettings.json              # Application settings
├── appsettings.Development.json  # Development settings
├── MySqlConnectionManager.cs     # Database connection management
├── Program.cs                    # Application entry point
├── PigeonInvadersAPI.csproj      # Project file
├── PigeonInvadersAPI.sln         # Solution file
└── README.md                     # This file
```

### Building the Solution

```bash
# Clean build
dotnet clean
dotnet build

# Release build
dotnet build --configuration Release
```

### Running Tests

Currently, the project does not include unit tests. To add testing:

1. Create a test project:
   ```bash
   dotnet new xunit -n PigeonInvadersAPI.Tests
   ```

2. Add test project reference:
   ```bash
   cd PigeonInvadersAPI.Tests
   dotnet add reference ../PigeonInvadersAPI.csproj
   ```

## 📚 Swagger Documentation

When running in development mode, Swagger UI is automatically enabled and provides:

- Interactive API documentation
- Ability to test endpoints directly from the browser
- Request/response schema definitions
- Example values

**Access Swagger UI**:
- HTTPS: `https://localhost:7189/swagger`
- HTTP: `http://localhost:5181/swagger`

**Features**:
- Try out API endpoints without writing code
- View request/response models
- See all available endpoints and their parameters
- Generate sample requests

## 🔍 Troubleshooting

### Common Issues

#### Database Connection Errors

**Problem**: "Unable to connect to any of the specified MySQL hosts"

**Solution**:
1. Verify MySQL server is running
2. Check connection string credentials in `MySqlConnectionManager.cs`
3. Ensure database `PigeonInvadersDB` exists
4. Verify MySQL port (default: 3306) is not blocked

#### Timer Format Errors

**Problem**: "Invalid Timer format. Please use 'HH:mm:ss'"

**Solution**:
- Ensure timer is in the format "HH:mm:ss" (e.g., "00:05:30")
- Hours, minutes, and seconds must be two digits
- Use leading zeros for single-digit values

#### Port Already in Use

**Problem**: "Failed to bind to address - address already in use"

**Solution**:
- Change the port in `Properties/launchSettings.json`
- Or kill the process using the port:
  ```bash
  # Find process
  netstat -ano | findstr :5181
  
  # Kill process (Windows)
  taskkill /PID <process_id> /F
  
  # Kill process (Linux/Mac)
  kill -9 <process_id>
  ```

#### SSL/HTTPS Issues in Development

**Problem**: Certificate errors when accessing HTTPS endpoints

**Solution**:
```bash
# Trust the development certificate
dotnet dev-certs https --trust
```

### Logging

Check console output for detailed error messages. The application logs:
- Successful database connections
- Database connection errors
- API request errors

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/YourFeature`
3. **Commit your changes**: `git commit -m 'Add some feature'`
4. **Push to the branch**: `git push origin feature/YourFeature`
5. **Open a Pull Request**

### Coding Guidelines

- Follow C# coding conventions
- Use meaningful variable and method names
- Add comments for complex logic
- Ensure code builds without errors
- Test endpoints before submitting PR

## 📄 License

This project is available for use and modification. Please check with the repository owner for specific license terms.

## 🔗 Links

- **Repository**: [https://github.com/Traveler3114/PigeonInvadersAPI](https://github.com/Traveler3114/PigeonInvadersAPI)
- **ASP.NET Core Documentation**: [https://learn.microsoft.com/aspnet/core](https://learn.microsoft.com/aspnet/core)
- **MySQL Documentation**: [https://dev.mysql.com/doc/](https://dev.mysql.com/doc/)
- **Swagger/OpenAPI**: [https://swagger.io/](https://swagger.io/)

---

**Made with ❤️ for the Pigeon Invaders game**
