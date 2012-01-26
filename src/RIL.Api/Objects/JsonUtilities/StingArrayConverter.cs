using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json;

namespace RIL.Objects.JsonUtilities
{
    public class StringArrayConverter: JsonConverter
    {
        #region Overrides of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string val = string.Empty;
            if (value is IEnumerable)
            {
                val = ((IEnumerable)value).Cast<string>().Aggregate(val, (current, element) => current + (element + ",")).TrimEnd(',');
            }
            else
            {
                throw new Exception("Expected IEnumerable object value.");
            }
            writer.WriteValue(val);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;
            string[] strings = reader.Value.ToString().Split(',');
            if (typeof(object) == typeof(string[]))
                return strings;
            return strings.ToList();

        }

        public override bool CanConvert(Type valueType)
        {
            return (typeof (IEnumerable).IsAssignableFrom(valueType));
        }

        #endregion
    }
}
