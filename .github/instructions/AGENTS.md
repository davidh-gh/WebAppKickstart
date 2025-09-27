---
applyTo: '**'
description: 'AI agent instructions for .NET project'
---

# AI Agent Instructions for WebAppKickstart

## Project Context
This is a .NET 9.0 solution with the following structure:
- **Core**: Domain entities and business logic
- **Domain**: Data access and repositories  
- **Infrastructure**: External service integrations
- **WebApi**: REST API controllers and endpoints
- **KickStartWeb**: MVC web application
- **HealthUIWeb**: Health monitoring dashboard
- **Aspire.AppHost**: Application orchestration

## Primary Guidelines

### Code Generation Standards
- Target .NET 9.0 framework exclusively
- Use C# features supported by .NET 9.0 (records, pattern matching, null-coalescing assignment)
- Follow solution conventions and patterns
- Implement proper dependency injection using DI system
- Use async/await for all I/O-bound operations

### File Encoding & Formatting
- Use 4 spaces for indentation (no tabs)
- Follow Allman brace style (braces on new lines)
- Limit lines to 200 characters
- Place `using` statements outside the namespace and remove unused usings
- Refer to .editorconfig for additional style rules

### Naming Conventions
- **Classes & Interfaces**: PascalCase (e.g., `UserService`, `IUserRepository`)
- **Methods & Properties**: PascalCase (e.g., `GetUserByIdAsync`)
- **Variables & Parameters**: camelCase (e.g., `userId`, `cancellationToken`)
- **Constants**: PascalCase (e.g., `DefaultTimeout`)
- **Test Classes**: Suffix with `Tests` and must be **public** for xUnit
- **Async Methods**: Suffix with `Async`
- **DTOs**: Suffix with `Dto`

### Testing Requirements
- All xUnit test classes **must be public**
- Use descriptive test method names: `{MethodName}When{StateUnderTest}Then{ExpectedBehavior}`
- Follow Arrange-Act-Assert (AAA) pattern with line spaces between sections
- Use Shouldly for assertions, Moq for mocking
- Each test must be independent and isolated
- Use builder pattern or factory methods for complex test data setup
- Write integration tests for modules and services
- Ensure tests cover edge cases and error scenarios

### Error Handling & Logging
- Use Microsoft.Extensions.Logging for all logging
- Implement structured exception handling
- Never swallow exceptions - log or rethrow appropriately
- Use Data Annotations or Fluent Validation for model validation
- Return appropriate HTTP status codes in API controllers

### Performance & Best Practices
- Use `IDistributedCache` for caching scenarios
- Implement pagination for large datasets
- Write efficient LINQ queries and avoid N+1 problems
- Use AutoMapper or manual mapping for DTO conversion
- Follow SOLID and DRY principles

## Agent Behavior Guidelines

### Code Modifications
- Always read files before editing to understand existing structure
- Use minimal, focused changes when editing files
- Validate changes by checking for compilation errors after edits
- Prefer existing patterns and conventions found in the solution
- Generate production-ready code that integrates seamlessly
- Production code must compile without errors
- Production code must be testable and maintainable

### Communication Style
- Be concise and actionable in responses
- Explain reasoning when making architectural decisions
- Ask for clarification only when requirements are genuinely ambiguous
- Use markdown formatting for code snippets in explanations
- Reference official Microsoft and ASP.NET Core documentation when applicable

### File Operations
- Never output code blocks unless explicitly requested - use file editing tools instead
- Never output terminal commands unless explicitly requested - use terminal execution tools instead
- Check for and fix compilation errors after making changes
- Organize files by feature or architectural layer (Domain, Application, Infrastructure, etc.)

## Security Guidelines

### API Security
- APIs should be secure by design and follow secure coding practices
- Implement proper authentication and authorization mechanisms
- Validate and sanitize all input data
- Return appropriate HTTP status codes without exposing sensitive information
- Use secure headers and configure CORS policies appropriately
- Implement rate limiting and throttling

### Application Security
- Use HTTPS and enforce SSL/TLS
- Follow OWASP security guidelines for web applications
- Implement proper error handling that doesn't expose sensitive information
- Use secure configuration practices
- Validate all external dependencies and inputs

### Documentation Requirements
- Provide XML comments for all public API controllers and DTOs
- Use Swagger/OpenAPI documentation for APIs
- Document security-related configurations and practices

## Restrictions
- Do not generate harmful, inappropriate, or copyrighted content
- Do not alter dependencies between application layers without explicit justification
- Do not change test class visibility from public to internal
- Do not violate established architectural patterns
- Do not use deprecated .NET features or libraries

## Quality Assurance
- Ensure all generated code compiles without errors
- Verify that tests remain public and executable
- Confirm proper async/await usage for I/O operations
- Validate that solution conventions are maintained
- Check that logging and error handling are properly implemented

When deviating from these standards is necessary, add inline comments explaining the reason for the deviation.
