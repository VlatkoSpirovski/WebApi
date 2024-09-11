This project is a .NET Web API application that manages stock data, user accounts, and comments. The system allows for managing different entities and their relationships efficiently.

Features
Stock Management: CRUD operations for managing stock items.
User Management: Handles user accounts and roles.
Comment System: Users can leave comments on stock items.
Entity Relationships: The system links users, comments, and stock data to provide a comprehensive view of the interaction between these entities.
Technologies Used
.NET 8.0 Web API: Backend framework used for building the API.
Entity Framework Core: For ORM (Object Relational Mapping) to interact with the database.
SQL Server: Database used for storing stock, comments, and user information.
Database Structure
The project contains three main tables that are interconnected:

Stock Table: Stores information about stock items (e.g., item name, price, and quantity).
Comment Table: Stores user comments on specific stock items. Each comment is linked to a user and a stock item.
Account Table (User Management): Manages user details, including login credentials and roles. Each user can leave multiple comments, and each comment is linked to a user and a stock item.
Entity Relationships
Stock and Comment: A stock item can have multiple comments.
User and Comment: A user can leave multiple comments, and each comment belongs to a user.
Getting Started
Prerequisites
.NET SDK (version 8.0 or higher)
SQL Server or any compatible relational database
Postman or a similar tool for API testing
Installation
Clone the repository:

bash
Copy code
git clone https://github.com/yourusername/your-repository-name.git
Navigate to the project folder:

bash
Copy code
cd your-repository-name
Restore the dependencies:

bash
Copy code
dotnet restore
Set up the database:

Update the appsettings.json file with your SQL Server connection string.

Run migrations to create the necessary tables:

bash
Copy code
dotnet ef database update
Run the application:

bash
Copy code
dotnet run
The API will be running at https://localhost:5001.

API Endpoints
Stock Endpoints:

GET /api/stock: Get all stock items
POST /api/stock: Add a new stock item
PUT /api/stock/{id}: Update a stock item
DELETE /api/stock/{id}: Delete a stock item
User Endpoints:

GET /api/users: Get all users
POST /api/users: Register a new user
PUT /api/users/{id}: Update user information
DELETE /api/users/{id}: Delete a user
Comment Endpoints:

GET /api/comments: Get all comments
POST /api/comments: Add a new comment
PUT /api/comments/{id}: Update a comment
DELETE /api/comments/{id}: Delete a comment
