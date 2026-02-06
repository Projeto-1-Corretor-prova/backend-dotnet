using System.Text.Json.Serialization;

namespace backend.enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CritereaEnum
{
    KEYWORD,
    SEMANTIC,
    EXAMPLE
}