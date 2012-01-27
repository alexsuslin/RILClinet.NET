using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RIL.Constants;
using RIL.Objects.JsonUtilities;

namespace RIL.Objects
{
    [DataContract]
    public class UserListOptions
    {
        #region Properties

        [DataMember(Name = Methods.Params.Count)]
        public int Count { get; set; }

        [DataMember(Name = Methods.Params.Page)]
        public int Page { get; set; }

        [DataMember(Name = Methods.Params.Format), JsonConverter(typeof(StringEnumConverter))]
        public Format Format { get; set; }

        [DataMember(Name = Methods.Params.Since), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Since { get; set; }

        [DataMember(Name = Methods.Params.State), JsonConverter(typeof(StringEnumConverter))]
        public State State { get; set; }

        [DataMember(Name = Methods.Params.MyAppOnly), JsonConverter(typeof(StringEnumConverter))]
        public RetreiveType RetreiveType { get; set; }

        [DataMember(Name = Methods.Params.Tags), JsonConverter(typeof(StringEnumConverter))]
        public RetrieveTags RetrieveTags { get; set; }

        #endregion
    }
}

    
