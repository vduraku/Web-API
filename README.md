# Aralytiks Web API

A .NET Web API project for managing Users and Posts with SQL Server(instead of oracle) database.

## Project Structure

The solution is organized into three main projects:

1. **Aralytiks.Domain**
   - Contains entities (User, Post)
   - DTOs for data transfer
   - Repository interfaces

2. **Aralytiks.Infrastructure**
   - Entity Framework Core DbContext
   - Repository implementations
   - Mappings
   - Services
   - Database migrations

3. **Aralytiks.API**
   - REST API controllers
   - Swagger documentation
   - Application configuration

## Setup Instructions

### Option 1: Using Local SQL Server

1. Open the solution in Visual Studio
2. In `Aralytiks.API/Program.cs`, ensure the connection string is set to use your local SQL Server:
   ```csharp
   options.UseSqlServer("Server=DESKTOP-K2LMBPP\\VALONA;Database=AralytiksDb;Trusted_Connection=True;TrustServerCertificate=True;")
   ```
3. Open Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console)
4. Set the default project to `Aralytiks.Infrastructure`
5. Run the following commands:
   ```
   Add-Migration InitialCreate
   Update-Database
   ```

### Option 2: Using Docker SQL Server

1. Make sure Docker Desktop is running
2. Open a terminal in the solution root directory
3. Run the following command to start SQL Server:
   ```bash
   docker-compose up -d
   ```
4. In `Aralytiks.API/Program.cs`, ensure the connection string is set to use Docker:
   ```csharp
   options.UseSqlServer("Server=localhost,1433;Database=AralytiksDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;")
   ```


## Running the Application

1. `Aralytiks.API` is as the startup project
3. The Swagger UI will be available at: `https://localhost:49701/swagger` (49701-my local port where I tested)

## API Endpoints

### Users
- `GET /api/users` - Get all users (paginated)
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create a new user
- `PUT /api/users/{id}` - Update a user
- `DELETE /api/users/{id}` - Delete a user

### Posts
- `GET /api/posts` - Get all posts (paginated)
- `GET /api/posts/{id}` - Get post by ID
- `POST /api/posts` - Create a new post
- `PUT /api/posts/{id}` - Update a post
- `DELETE /api/posts/{id}` - Delete a post

### First Create some Users afterwards Posts (because of connection between tables)


## Troubleshooting

1. ** Check connection string in Program.cs
   

2. ** If any Migration Errors**
   - Clean and rebuild the solution
   - Delete existing migrations if needed
   - Ensure correct project is selected in Package Manager Console
# Web-API
