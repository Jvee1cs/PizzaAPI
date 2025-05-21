# Pizza Store API

A RESTful API for managing pizzas and toppings built with ASP.NET Core 8.

## Features

- Manage toppings (CRUD operations)
- Manage pizzas (CRUD operations)
- Many-to-many relationship between pizzas and toppings
- Input validation and duplicate prevention
- Proper HTTP status codes and error handling

## Getting Started

### Prerequisites

- .NET 8 SDK
- Your favorite IDE (Visual Studio, VS Code, etc.)

### Installation

1. Clone the repository
2. Navigate to the project directory
3. Run `dotnet restore` to restore dependencies

### Running the API

```bash
dotnet run
The API will be available at:

https://localhost:5001

http://localhost:5000

or
http://localhost:5227

Testing

Use tools like Postman or Swagger UI (available at /swagger) to test the endpoints.
http://localhost:5227/swagger

API Endpoints

Toppings
GET /api/toppings - List all toppings

GET /api/toppings/{id} - Get a specific topping

POST /api/toppings - Add a new topping

PUT /api/toppings/{id} - Update a topping

DELETE /api/toppings/{id} - Delete a topping

Pizzas
GET /api/pizzas - List all pizzas with toppings

GET /api/pizzas/{id} - Get a specific pizza

POST /api/pizzas - Add a new pizza

PUT /api/pizzas/{id} - Update pizza details

PATCH /api/pizzas/{id}/toppings - Update pizza toppings

DELETE /api/pizzas/{id} - Delete a pizza

Database
The API uses an in-memory database that resets when the application restarts. For a persistent database, you can configure SQL Server or SQLite in Program.cs.

This solution provides a complete implementation of the pizza store API with proper RESTful design, validation, error handling, and documentation. The code is organized with separation of concerns in mind, using controllers, services, and repositories pattern.

ALL CREATED BY JAKE BALORAN