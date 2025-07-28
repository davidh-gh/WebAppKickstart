# WebAppKickstart

## Pipeline
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
