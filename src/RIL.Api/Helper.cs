using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace RIL
{
    internal static class Helper
    {
        public static string GetValueByName(this IList<Parameter> list, string name)
        {
            Parameter p = list.FirstOrDefault(parameter => parameter.Name == name);
            return p != null ? p.Value.ToString() : null;
        }

        public static IRestRequest AddDesrelializedObject<T>(this RestRequest request, T obj)
        {
            string serialized = request.JsonSerializer.Serialize(obj);
            JObject deserializeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(serialized);
            foreach (KeyValuePair<string, JToken> pair in deserializeObject)
            {
                request.AddParameter(pair.Key, pair.Value.Value<string>());
            }
            return request;
        }

        /// <summary>
        /// Create from list of items a dictionary where key is from 0 to N.
        /// That is needed to fit json requirements for RIL.
        /// {
        ///     "0" : { ... item object ... },
        ///     "1" : { ... item object ... },
        ///     "2" : { ... item object ... },
        ///     etc
        /// }
        /// 
        /// This is because they use associated massive and convert json array as object
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Dictionary<int, T> ConvertToDictionary<T>(IList<T> items)
        {
            int i = 0;
            return items.ToDictionary(item => i++);
        }

        public static IList<T> ConvertToList<T>(IDictionary<object, T> dic)
        {
            return dic.Select(item => item.Value).ToList();
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTimeSpan(DateTime dateTime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (dateTime - dtDateTime).TotalSeconds;
        }

        public static string GetEnumStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            EnumValueAttribute[] attrs = fi.GetCustomAttributes(typeof(EnumValueAttribute), false) as EnumValueAttribute[];
            if (attrs != null && attrs.Length > 0)
                output = attrs[0].Value;

            return output;
        }

        #region Newtonsoft.Json/Utilities

        public static bool IsNullable(Type t)
        {
            ArgumentNotNull(t, "t");

            if (t.IsValueType)
                return IsNullableType(t);

            return true;
        }

        public static bool IsNullableType(Type t)
        {
            ArgumentNotNull(t, "t");

            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof (Nullable<>));
        }

        public static void ArgumentNotNull(object value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            ArgumentNotNull(format, "format");

            return string.Format(provider, format, args);
        }

        #endregion

    }
}
