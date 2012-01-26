using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace RIL.Objects.JsonUtilities
{
    public class ObjectsArrayConverter<T>: JsonConverter
    {
        #region Overrides of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            // Create from list of items a dictionary where key is from 0 to N.
            // That is needed to fit json requirements for RIL.
            // {
            //     "0" : { ... item object ... },
            //     "1" : { ... item object ... },
            //     "2" : { ... item object ... },
            //     etc
            // }
            // 
            // This is because they use associated massive and convert json array as object
            int i = 0;
            serializer.Serialize(writer, ((IList<T>)value).ToDictionary(item => i++));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Helper.ConvertToList(serializer.Deserialize<IDictionary<object, T>>(reader));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (IList<T>);
        }

        #endregion
    }
}
