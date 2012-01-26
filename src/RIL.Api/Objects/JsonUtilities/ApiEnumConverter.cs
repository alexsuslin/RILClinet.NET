using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace RIL.Objects.JsonUtilities
{
    internal class ApiEnumConverter: StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                EnumValueAttribute attr = fi.GetCustomAttributes(typeof(EnumValueAttribute), false).Cast<EnumValueAttribute>().FirstOrDefault();
                
                if (attr != null && !string.IsNullOrEmpty(attr.Value))
                {
                    writer.WriteValue(attr.Value);
                    return;
                }
            }

            base.WriteJson(writer, value, serializer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            FieldInfo info = objectType.GetFields().FirstOrDefault(
                fieldInfo => fieldInfo
                                 .GetCustomAttributes(typeof (EnumValueAttribute), false)
                                 .Cast<EnumValueAttribute>()
                                 .FirstOrDefault(attr => attr.Value == reader.Value.ToString()) != null);
            if(info != null)
            {
                return Enum.Parse(objectType, info.GetValue(info).ToString(), true);
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}
