namespace WebApi.Code.Monitor
{
    public static class LogMessages
    {
        // HealthCheckController
        public static readonly Action<ILogger, Exception?> LogHealthPing =
            LoggerMessage.Define(LogLevel.Information, new EventId((int)LogEvents.HealthPing, "Ping"), "Pinging the health endpoint");

        // AuthenticationController
        public static readonly Action<ILogger, string, Exception?> LogAuthenticationRequest =
            LoggerMessage.Define<string>(LogLevel.Debug, new EventId((int)LogEvents.AuthenticationToken, "GetAuthentication"),
                "Get token for {UserName}");

        // DemosController
        public static readonly Action<ILogger, int, Exception?> LogDemosGetById =
            LoggerMessage.Define<int>(LogLevel.Debug, new EventId((int)LogEvents.DemosGetById, "GetDemosById"),
                "Get demo by id {Id}");
        public static readonly Action<ILogger, int, Exception?> LogDemosGetByIdInvalidId =
            LoggerMessage.Define<int>(LogLevel.Warning, new EventId((int)LogEvents.DemosGetById, "GetDemosById-InvalidId"),
                "Invalid input: {Id}. Must be between 0 and 100");

        public static readonly Action<ILogger, Exception?> LogDemosGet =
            LoggerMessage.Define(LogLevel.Debug, new EventId((int)LogEvents.DemosGet, "GetDemos"),
                "Get all demos");

        // WeatherForecastController
        public static readonly Action<ILogger, int, Exception?> LogWeatherForecastRequest =
            LoggerMessage.Define<int>(LogLevel.Debug, new EventId((int)LogEvents.WeatherForecastGet, "GetWeatherForecast"),
                "Get weather forecast for {Count} days");
    }
}