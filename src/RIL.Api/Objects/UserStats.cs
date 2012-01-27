using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RIL.Constants;
using RIL.Objects.JsonUtilities;

namespace RIL.Objects
{
    [DataContract]
    public class UserStats
    {
        #region Properties

        [DataMember(Name = Methods.Params.CountUnreadItems)]
        public int NumberOfUnreadItems { get; set; }

        [DataMember(Name = Methods.Params.CountItems)]
        public int NumberOfItems { get; set; }

        [DataMember(Name = Methods.Params.CountReadItems)]
        public int NumberOfReadItems { get; set; }

        [DataMember(Name = Methods.Params.UserSince), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime UserSince { get; set; }

        #endregion
    }
}
