# WebAppKickstart

## Pipeline

### SonarCloud
1. Create new project in SonarCloud
2. Add SonarCloud token to GitHub secrets
3. Add `coverlet.collector` nuget package to Tests projects
4. Add `coverlet.runsettings` to root of the Tests project
   ```xml
   <?xml version="1.0" encoding="utf-8"?>
   <RunSettings>
     <DataCollectionRunSettings>
       <DataCollectors>
         <DataCollector friendlyName="XPlat Code Coverage">
           <Configuration>          
                <Format>opencover,json,lcov</Format>
                <OutputDirectory>TestResults</OutputDirectory>
                <OutputName>coverage</OutputName>
           </Configuration>
         </DataCollector>
       </DataCollectors>
     </DataCollectionRunSettings>
   </RunSettings>
   ```
5. Running `dotnet test` will generate 
   * `coverage.xml` (cobertura format)
   * `coverage.json` (as coverlet for qodana) 
   * `coverage.opencover.xml` for sonarcloud code coverage
6. Upload `coverage.opencover.xml` to SonarCloud

### Qodana
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
