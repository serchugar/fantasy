namespace Fantasy.Backend.Configuration.AppSettings;

public class AppSemanticVersion
{
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }
    public required Prerelease Prerelease { get; set; }
    public string? Build { get; set; }
    
    public override string ToString()
    {
        string semver = $"{Major}.{Minor}";

        if (!string.IsNullOrWhiteSpace(Prerelease.Label))
        {
            semver += $".{Patch}";
            semver += $"-{Prerelease.Label}.{Prerelease.Iteration}";
        }
        else if (Patch != 0)
        {
            semver += $".{Patch}";
        }
        if (!string.IsNullOrWhiteSpace(Build)) semver += $"+{Build}";
        
        return semver;
    }
}

public class Prerelease
{
    public string? Label { get; set; }
    public int? Iteration { get; set; }
}
