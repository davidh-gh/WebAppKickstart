namespace WebApi.Code.Monitor
{
    internal enum LogEvents
    {
        None = 0,
        HealthPing = 1,
        AuthenticationToken = 10,
        DemosGetById = 100,
        DemosGet = 101,
        DemosPost = 102,
        DemosPut = 103,
        DemosPatch = 104,
        DemosDelete = 105,
        SitesGet = 200,
        WeatherForecastGet = 1001
    }
}
