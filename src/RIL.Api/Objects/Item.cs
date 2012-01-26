using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RIL.Constants;
using RIL.Objects.JsonUtilities;

namespace RIL.Objects
{
    [DataContract]
    public class Item
    {
        #region Properties

        [DataMember(Name = Methods.Params.Url)]
        public string Url { get; set; }

        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }

        [DataMember(Name = Methods.Params.RefID)]
        public string RefID { get; set; }

        [DataMember(Name = Methods.Params.Tags), JsonConverter(typeof (StringArrayConverter))]
        public List<string> Tags { get; set; }

        [DataMember(Name = Methods.Params.ItemID)]
        public int ItemID { get; set; }

        [DataMember(Name = Methods.Params.State)]
        public int State { get; set; }

        [DataMember(Name = Methods.Params.TimeUpdated), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeUpdated { get; set; }

        [DataMember(Name = Methods.Params.TimeAdded), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeAdded { get; set; }

        #endregion
        
        #region Constructors

        public Item(string url, string title = null, string refID = null)
        {
            Url = url;
            Title = title;
            RefID = refID;
        }

        #endregion
    }
}
