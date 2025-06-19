namespace Fantasy.Backend.Configuration.AppSettings;

public class ServiceConfig
{
    [ConfigurationKeyName("AppSemanticVersion")]
    public required AppSemanticVersion AppVersion { get; set; }
}
