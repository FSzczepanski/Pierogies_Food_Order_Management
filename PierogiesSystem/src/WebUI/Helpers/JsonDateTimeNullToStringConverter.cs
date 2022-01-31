namespace CleanArchitecture.WebUI.Helpers
{
    using global::System;
    using global::System.Text.Json;
    using global::System.Text.Json.Serialization;

    public class JsonDateTimeNullToStringConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                {
                    if (DateTime.TryParse(reader.GetString(), out var parsedDateTime))
                    {
                        return parsedDateTime.ToUniversalTime();
                    }

                    return null; 
                }
                default:
                    throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value?.ToString("u") ?? "");    
            }
            else
            {
                writer.WriteNullValue();
            }
            
        }
    }
}
