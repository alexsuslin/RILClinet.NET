using System;
using System.Collections.Generic;
using System.Linq;
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
        public static Dictionary<int, Item> ConvertToDictionary(IList<Item> items)
        {
            int i = 0;
            return items.ToDictionary(item => i++);
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
    }
}
