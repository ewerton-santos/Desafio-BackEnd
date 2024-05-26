using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RentBike.Domain
{
    public class StringEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : Enum
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException($"Unexpected token {reader.TokenType}.");
            }

            string enumString = reader.GetString();
            foreach (var enumValue in Enum.GetValues(typeToConvert))
            {
                var enumMemberAttribute = typeof(TEnum)
                    .GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(EnumMemberAttribute), true)
                    .FirstOrDefault() as EnumMemberAttribute;

                if (enumMemberAttribute != null && enumMemberAttribute.Value == enumString)
                {
                    return (TEnum)enumValue;
                }
            }

            throw new JsonException($"Unknown string value: {enumString}.");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            var enumMemberAttribute = typeof(TEnum)
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(EnumMemberAttribute), true)
                .FirstOrDefault() as EnumMemberAttribute;

            if (enumMemberAttribute != null)
            {
                writer.WriteStringValue(enumMemberAttribute.Value);
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
