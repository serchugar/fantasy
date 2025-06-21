using System.Text.Json;

namespace Fantasy.Frontend.Configuration;

public static class JsonSerializerConfig
{
    public static JsonSerializerOptions? Opts => new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
}