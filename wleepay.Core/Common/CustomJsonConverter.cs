using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace wleepay.Core.Common
{

    public class CustomJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(object) || objectType == typeof(List<object>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<object>>();
            }
            return token.ToObject<object>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is List<object> list)
            {
                JArray array = JArray.FromObject(list);
                array.WriteTo(writer);
            }
            else
            {
                JObject obj = JObject.FromObject(value);
                obj.WriteTo(writer);
            }
        }
    }

}
