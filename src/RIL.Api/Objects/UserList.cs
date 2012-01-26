using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RIL.Constants;
using RIL.Objects.JsonUtilities;

namespace RIL.Objects
{
    [DataContract]
    public class UserList
    {
        #region Properties

        [DataMember(Name = Methods.Params.Complete)]
        public int Complete { get; set; }

        [DataMember(Name = Methods.Params.Since), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Since { get; set; }

        [DataMember(Name = Methods.Params.Status), JsonConverter(typeof(StringEnumConverter))]
        public UserStatus Status { get; set; }

        [DataMember(Name = Methods.Params.List), JsonConverter(typeof(ObjectsArrayConverter<Item>))]
        public IList<Item> List { get; set; }

        #endregion
    }
}
