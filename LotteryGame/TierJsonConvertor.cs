using LotteryGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class TierJsonConverter : JsonConverter<TierBase>
    {
        public override TierBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                var root = jsonDoc.RootElement;

                if (root.TryGetProperty("WinningTicketsCount", out var _))
                {
                    return jsonDoc.Deserialize<GrandPrizeTier>(options);
                }

                return jsonDoc.Deserialize<SecondaryTier>(options);
            }
        }

        public override void Write(Utf8JsonWriter writer, TierBase value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

    }

    public class DecimalJsonConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDecimal();
            }

            if (reader.TokenType == JsonTokenType.String && decimal.TryParse(reader.GetString(), out var value))
            {
                return value;
            }

            throw new JsonException($"Unable to convert {reader.GetString()} to decimal.");
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }

    public class IntJsonConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32(); // If it's a numeric value, just return it.
            }

            if (reader.TokenType == JsonTokenType.String && int.TryParse(reader.GetString(), out var value))
            {
                return value; // Parse the string into an integer.
            }

            throw new JsonException($"Unable to convert {reader.GetString()} to int.");
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value); // Write the integer as a number in JSON.
        }
    }
}
