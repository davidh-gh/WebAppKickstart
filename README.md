# WebAppKickstart

To get started, create repository from this template.

Structure:
* MySolution.sln
* src/Core/Core.proj
* src/Domain/Domain.proj
* src/Infrastructure/Infrastructure.proj
* src/Web/Web.proj
* src/Api/Api.proj
* test/UnitTests.proj
* test/AcceptanceTests.proj
* db/LocalDb.db

Add as solution items:
* .editorconfig - to keep code style consistent
* Directory.Build.props - to keep project settings consistent
* Directory.Build.targets - to keep project packages consistent

### Pipeline
1. Create new project in SonarCloud
2. Add SonarCloud token to GitHub secrets
3. Create new project in Qodana
4. Add Qodana token to GitHub secrets
5. Copy .github/workflows/ or .gitlab-ci.yml to your repository
6. Test

### Interesting
1. Check Api/Code/LogEvents enum for defining LoggerMessage
2. Check definition and setting EventId for LoggerMessage

ToDo:
* Serilog
* OpenTelemetry -> Grafana
* HealthChecks
* Tracing
* OData
