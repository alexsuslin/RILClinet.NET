using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RIL.Objects.JsonUtilities
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        private string[] _dateTimeFormat;

        public new string[] DateTimeFormat
        {
            get { return _dateTimeFormat ?? new[] { DefaultDateTimeFormat }; }
            set { _dateTimeFormat = value; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool nullable = Helper.IsNullableType(objectType);
            Type t = (nullable)
              ? Nullable.GetUnderlyingType(objectType)
              : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!Helper.IsNullableType(objectType))
                    throw new Exception("Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));

                return null;
            }

            if (reader.TokenType != JsonToken.String)
                throw new Exception("Unexpected token parsing date. Expected String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));

            string dateText = reader.Value.ToString();

            if (string.IsNullOrEmpty(dateText) && nullable)
                return null;

#if !PocketPC && !NET20
            if (t == typeof(DateTimeOffset))
            {
                if (_dateTimeFormat != null)
                    return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, DateTimeStyles);
                return DateTimeOffset.Parse(dateText, Culture, DateTimeStyles);
            }
#endif


            if (_dateTimeFormat != null)
                return DateTime.ParseExact(dateText, _dateTimeFormat, Culture, DateTimeStyles);
            else
                return DateTime.Parse(dateText, Culture, DateTimeStyles);
        }
    }
}
