namespace CleanArchitecture.WebUI.Helpers
{
    using global::System;
    using global::System.Text.Json;
    using global::System.Text.Json.Serialization;

    public class StringConverterHelper : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                {
                    var stringValue = reader.GetInt32();
                    return stringValue.ToString();
                }
                case JsonTokenType.String:
                    return reader.GetString();
                default:
                    throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
