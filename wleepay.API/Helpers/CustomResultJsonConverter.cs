using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using wleepay.Core.DTO.Response;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

using CoreResult = wleepay.Core.DTO.Response.Result; //alias 

namespace wleepay.API.Helpers
{
    public class CustomResultJsonConverter : JsonConverter<CoreResult>
    {
        public override CoreResult ReadJson(JsonReader reader, Type objectType, CoreResult existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                // If the result is an object, deserialize it as a Result object
                JObject obj = JObject.Load(reader);
                return obj.ToObject<CoreResult>();
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                // If the result is an array, consume the array and return null
                JArray array = JArray.Load(reader);
                if (array.Count == 0)
                {
                    return null;
                }
            }
            throw new JsonSerializationException("Unexpected token type for result property");
        }

        public override void WriteJson(JsonWriter writer, CoreResult value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteStartArray();
                writer.WriteEndArray();
                return;
            }

            writer.WriteStartObject();

            writer.WritePropertyName("id");
            writer.WriteValue(value.Id);

            writer.WritePropertyName("operator");
            serializer.Serialize(writer, value.Operator);

            writer.WritePropertyName("country");
            serializer.Serialize(writer, value.Country);

            writer.WritePropertyName("amount");
            serializer.Serialize(writer, value.Amount);

            writer.WritePropertyName("currency");
            serializer.Serialize(writer, value.Currency);

            writer.WritePropertyName("productId");
            writer.WriteValue(value.ProductId);

            writer.WritePropertyName("productType");
            writer.WriteValue(value.ProductType);

            writer.WritePropertyName("simulation");
            writer.WriteValue(value.Simulation);

            writer.WritePropertyName("userReference");
            writer.WriteValue(value.UserReference);

            writer.WritePropertyName("accountId");
            writer.WriteValue(value.AccountId);

            writer.WritePropertyName("balance");
            serializer.Serialize(writer, value.Balance);

            writer.WritePropertyName("instructions");
            writer.WriteValue(value.Instructions);

            writer.WriteEndObject();
        }
    }

}

