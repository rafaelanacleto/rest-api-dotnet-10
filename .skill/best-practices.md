# SKILL: Best Practices & project-standards

This document outlines the architectural patterns, "what, how, and why" of the проект, and established coding standards to maintain consistency across future features.

## 🎯 What the Project Does
This is a standard REST API built with .NET 10, designed to be a robust, scalable, and maintainable template for managing entities (e.g., `Person`). It provides endpoints for CRUD operations, integrated documentation (Swagger), and a deployment-ready configuration (Docker, Azure Pipelines).

## 🏛️ Architecture: How & Why
The project follows a **3-Tier Architecture** (Separation of Concerns):
- **Controllers**: Handle HTTP requests and responses. (Why: Decouple API contracts from business logic).
- **Services**: Contain business rules and orchestrate operations. (Why: Centralize logic and facilitate unit testing).
- **Repositories**: Direct interaction with the database via Entity Framework Core. (Why: Abstract data access and allow switching data sources without affecting logic).

---

## 🛠️ Coding Standards

### 📝 Naming Conventions
- **Interfaces**: Must always be prefixed with `I`.
    - *Example*: `IPersonRepository` is implemented by `PersonRepository`.
- **Controllers**: Suffix with `Controller`.
    - *Example*: `PersonController`.

### 💉 Dependency Injection
- Use **Scoped** lifetime for Services and Repositories in `Program.cs`.
    - *Example*: `builder.Services.AddScoped<IPersonServices, PersonServices>();`

### ⚡ Async/Await Patterns (Critical)
Always use asynchronous programming to ensure performance and prevent thread blocking.
- **DO**: Use `async` and `await`.
- **DON'T**: Use `.Result` or `.Wait()`.
- *Correct*: `var people = await _repository.GetAllAsync();`
- *Incorrect*: `var people = _repository.GetAllAsync().Result;`

### 📂 File Structure
- `Model/`: POCO classes representing database entities.
- `Context/`: EF Core `DbContext` configuration.
- `Repository/`: Data access logic.
- `Services/`: Business logic layer.
- `Controllers/`: API entry points.

---

## 🚀 Guidelines for New Features
1. **Repository First**: Define the repository interface and implementation for data access.
2. **Service Layer**: Wrap the repository in a service to handle any business logic.
3. **Controller**: Expose the service via REST endpoints using `IActionResult`.
4. **DI Registration**: Don't forget to register new services/repositories in `Program.cs`.
5. **XML Comments**: Add three-slash comments (`///`) to controller methods for Swagger documentation.
