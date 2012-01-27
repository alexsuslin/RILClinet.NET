using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RIL.Constants;
using RIL.Objects.JsonUtilities;

namespace RIL.Objects
{
    [DataContract]
    public class SendChangesParams
    {
        [DataMember(Name = Methods.Params.New), JsonConverter(typeof(ObjectsArrayConverter<Item>))]
        public IList<Item> NewItems { get; set; }
        [DataMember(Name = Methods.Params.Read), JsonConverter(typeof(ObjectsArrayConverter<Item>))]
        public IList<Item> ReadItems { get; set; }
        [DataMember(Name = Methods.Params.UpdateTitles), JsonConverter(typeof(ObjectsArrayConverter<Item>))]
        public IList<Item> UpdateTitles { get; set; }
        [DataMember(Name = Methods.Params.UpdateTags), JsonConverter(typeof(ObjectsArrayConverter<Item>))]
        public IList<Item> UpdateTags { get; set; }
    }
}
