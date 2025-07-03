# LiquidLabsAssessmentRepo

# 📦 Public API to SQL Server - ASP.NET Core Web API

This project is a .NET 8 Web API that fetches user data from a public API (`https://jsonplaceholder.typicode.com/users`), stores it in a local SQL Server database using raw SQL (no ORM), and exposes endpoints to retrieve that data.

---

## ✅ Features

- 🔌 Fetch data from a public API
- 🗄️ Store and retrieve data using SQL Server (raw SQL, no Entity Framework)
- ⚙️ Advanced error handling using custom middleware and exceptions
- 📡 RESTful API endpoints (`GET /api/users`, `GET /api/users/{id}`)
- 🧠 `//GenAI` marker used to indicate AI-generated code lines
- 📁 Self-contained: schema, code, and setup instructions in one repo
- 
- ---

## 🧰 Tech Stack

| Component    | Technology        |
|--------------|-------------------|
| Framework    | .NET 8 (ASP.NET Core Web API) |
| Language     | C#                |
| DB Access    | `System.Data.SqlClient` (raw SQL) |
| Database     | SQL Server (local instance) |
| HTTP Client  | `HttpClientFactory` |
| Error Handling | Custom middleware + exceptions |

---

## 📁 Project Structure

<pre> LiquidLabslApp/
├── Controllers/
├── Models/
├── Services/
├── Data/
├── Exceptions/
├── Middlewares/
├── sql/
│ └── create_tables.sql <-- SQL schema
├── appsettings.json
├── Program.cs
├── README.md </pre>

---

## 🗄️ SQL Server Setup
  
### 2. Create Database and Table

1. Open your MSSQL
2. Run:

CREATE DATABASE UserDb;
GO

USE UserDb;
GO
Run schema from sql/create_tables.sql:

sql
Copy
Edit
CREATE TABLE Users (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Username NVARCHAR(100),
    Email NVARCHAR(100)
);

### 3. appsettings.json database connection string 

"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=UserDb;User Id=sa;Password=YourStrong!Passw0rd;"
  },
