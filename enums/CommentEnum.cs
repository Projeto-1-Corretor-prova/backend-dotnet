using System.Text.Json.Serialization;

namespace backend.enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CommentEnum
{
    AI,
    TEACHER
}