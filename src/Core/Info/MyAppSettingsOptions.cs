namespace Core.Info
{
    public class MyAppSettingsOptions
    {
        // there are 5 core settings levels, lower levels override higher levels:
        // 1. Application settings (e.g., appsettings.json, appsettings.Development.json)
        // 2. Environment-specific json file settings (e.g., appsettings.Production.json, appsettings.Staging.json)
        // 3. User secrets (e.g., for development, stored in a secure location)
        // 4. Environment variables
        // 5. Command line arguments

        public string? Setting1 { get; set; }

        public string? Setting2 { get; set; }

        public bool IsEnabled { get; set; }
    }
}
