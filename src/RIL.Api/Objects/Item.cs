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
        #region Fields

        private readonly IList<string> tagmas;

        #endregion

        #region Properties

        [DataMember(Name = Methods.Params.TimeAdded)]
        protected internal double TimeAddedUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(TimeAdded); }
            set { TimeAdded = Helper.UnixTimeStampToDateTime(value); }
        }

        [DataMember(Name = Methods.Params.Url)]
        public string Url { get; set; }

        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }

        [DataMember(Name = Methods.Params.RefID)]
        public string RefID { get; set; }

        [DataMember(Name = Methods.Params.Tags)]
        public string Tags
        {
            get { return tagmas.Aggregate(string.Empty, (current, tagma) => current + (tagma + ',')).TrimEnd(','); }
            set { AddTags(value.Split(',')); }
        }

        [DataMember(Name = Methods.Params.ItemID)]
        public int ItemID { get; set; }

        [DataMember(Name = Methods.Params.State)]
        public int State { get; set; }

        [DataMember(Name = Methods.Params.TimeUpdated), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeUpdated { get; set; }

        public DateTime TimeAdded { get; set; }

        #endregion
        
        #region Constructors

        public Item(string url, string title = null, string refID = null)
        {
            Url = url;
            Title = title;
            RefID = refID;
            tagmas = new List<string>();
        }

        #endregion

        #region Helper Methods

        public void AddTags(params string[] tags2add)
        {
            foreach (string tag in tags2add)
            {
                tagmas.Add(tag);
            }
        }

        #endregion
    }
}
