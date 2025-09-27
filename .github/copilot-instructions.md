# .NET Copilot Instructions

## Code Style and Structure
- Write concise, idiomatic C# code.
- Follow folder and module structure (e.g., *.Application, *.Domain, *.EntityFrameworkCore, *.HttpApi).
- Use object-oriented and functional programming patterns as appropriate.
- Prefer LINQ and lambda expressions for collections.
- Use descriptive variable and method names.
- Adhere to modular development approach to separate concerns between layers.

## Naming Conventions
- PascalCase for class names, method names, and public members.
- camelCase for local variables and private fields.
- Prefix interface names with "I" (e.g., IUserService).
- If a class is a service, repository, or manager, suffix its name accordingly (e.g., UserService, OrderRepository).
- Use meaningful names for methods that clearly indicate their purpose (e.g., GetUserById).
- Use abbreviations sparingly and only when widely recognized (e.g., Id for Identifier).
- Use singular nouns for class names representing entities (e.g., User, Product).
- Use plural nouns for collections (e.g., Users, Products).
- Use constants or enums for fixed values instead of magic numbers or strings.
- Use async suffix for asynchronous methods (e.g., GetUserAsync).
- Use Dto suffix for Data Transfer Objects (e.g., UserDto).
- Use Tests suffix for unit test classes (e.g., UserServiceTests).
- If naming conflicts arise, use more descriptive names rather than abbreviations. If unclear use Microsoft's naming guidelines.

## C# and .NET Usage
- Use C# features supported by .NET 9.0 (e.g., record types, pattern matching, null-coalescing assignment).
- Leverage ASP.NET Core features and middleware.
- Use Entity Framework Core with repository abstractions.

## Syntax and Formatting
- Follow C# Coding Conventions.
- Use expressive syntax (null-conditional, string interpolation).
- Use `var` for implicit typing when the type is obvious.
- Keep code clean and consistent.
- Follow SOLID and DRY principles.

## Error Handling and Validation
- Use exceptions for exceptional cases only.
- Implement error logging with Microsoft.Extensions.Logging.
- Use Data Annotations or Fluent Validation for model validation.
- Leverage global exception handling middleware.
- Return appropriate HTTP status codes and consistent error responses in API controllers. Follow the CRUD operation conventions (e.g., 200 OK, 201 Created, 204 No Content, 400 
  Bad Request, 404 Not Found, 500 Internal Server Error), and use recommended http methods (GET, POST, PUT, DELETE, PATCH).

## API Design
- Follow RESTful API design principles.
- Use conventional HTTP API controllers and attribute-based routing.
- Integrate API versioning if needed.
- Use action filters or middleware for cross-cutting concerns.

## Performance Optimization
- Use async/await for I/O-bound operations.
- Use IDistributedCache for caching.
- Write efficient LINQ queries and avoid N+1 problems.
- Implement pagination for large data sets.

## Key Conventions
- Use Dependency Injection system.
- Use repository pattern or EF Core directly as appropriate.
- Use AutoMapper or object mapping for DTOs.
- Implement background tasks with IHostedService or background job systems.
- Keep business rules in the Domain layer.
- Avoid unnecessary dependencies.
- Do not alter dependencies between application layers.

## Testing
- Use xUnit for unit testing.
- Use Shouldly for assertions and Moq for mocking.
- Implement integration tests for modules and services.

## Security
- APIs should be secure by design and follow secure coding practices.
- Implement proper authentication and authorization mechanisms.
- Use HTTPS and enforce SSL/TLS.
- Validate and sanitize all input data.
- Implement rate limiting and throttling.
- Use secure headers and configure CORS policies appropriately.
- Follow OWASP security guidelines for web applications.
- Implement proper error handling that doesn't expose sensitive information.
- When auditing is required, log security-relevant events (e.g., login attempts, data access).
- When logging sensitive data, ensure compliance with data protection regulations (e.g., GDPR) and sanitize logs to avoid exposing sensitive information.

## API Documentation
- Use Swagger/OpenAPI for API documentation.
- Provide XML comments for all public API controllers and DTOs.
- Follow Microsoft's guidelines for documenting APIs and services.

Refer to official Microsoft and ASP.NET Core documentation for best practices in routing, controllers, and other .NET components.

### General Principles
- **Target Framework:** All projects target .NET 9.0. Ensure compatibility with this version.
- **Language:** Use C# for all code unless otherwise specified. (unless frontend code)
- **Code Clarity:** Prioritize clear and maintainable code over clever or complex solutions.
- **Readability:** Write clear, self-explanatory code. Use meaningful variable, method, and class names.
- **Consistency:** Follow consistent naming conventions and code formatting throughout the solution.
- **Comments:** Add inline comments only where necessary to clarify complex logic.
- **Error Handling:** Use structured exception handling. Avoid swallowing exceptions; log or rethrow as appropriate.
- **SOLID Principles:** Adhere to SOLID design principles for maintainable and extensible code.
- **Dependency Injection:** Prefer constructor injection for dependencies.
- **Async/Await:** Use asynchronous programming patterns where appropriate, especially for I/O-bound operations.
- **Magic Numbers:** Avoid magic numbers; use named constants or enums.
- **File Organization:** One class per file. Organize files into appropriate folders by feature or layer.

### Naming Conventions
- **Classes & Interfaces:** PascalCase (e.g., `UserService`, `IUserRepository`)
- **Methods & Properties:** PascalCase (e.g., `GetUserById`)
- **Variables & Parameters:** camelCase (e.g., `userId`)
- **Constants:** PascalCase (e.g., `DefaultTimeout`)

### Unit Test Naming Conventions
- **Test Classes:** Name test classes after the class they are testing, suffixed with `Tests` (e.g., `UserServiceTests`). 
- All xUnit test classes must be public.
- **Unit Test Methods:** Use descriptive names indicating the scenario and expected outcome (e.g., `{MethodName}When{StateUnderTest}Then{ExpectedBehavior}Test`).
- **Test Structure:** Follow Arrange-Act-Assert (AAA) pattern in all tests. Should have line space between each section and no comments indicating sections.
- **Isolation:** Each test should be independent and not rely on the outcome of other tests.
- **Mocking:** Use mocks or fakes for external dependencies (e.g. APIs) to ensure tests are deterministic.

### Code Style
- **Braces:** Use Allman style (braces on new lines).
- **Indentation:** Use 4 spaces per indentation level.
- **Line Length:** Limit lines to 200 characters.
- **Usings:** Place `using` statements outside the namespace and remove unused usings.
- Refer to .editorconfig for additional style rules.

## Copilot Usage

- **Adhere to these standards** when generating or modifying code.
- **Prefer existing patterns** and conventions found in the solution.
- **Generate code that is ready to use** and fits seamlessly into the current structure.
- **If a deviation from these standards is necessary, add a comment in the generated code explaining the reason.**
