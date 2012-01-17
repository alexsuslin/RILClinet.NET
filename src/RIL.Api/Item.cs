using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RIL
{
    [DataContract]
    public class Item
    {
        private readonly IList<string> tagmas;

        [DataMember(Name = Methods.Params.Url)]
        public string Url { get; set; }
        
        [DataMember(Name = Methods.Params.Title)]
        public string Title { get; set; }
        
        [DataMember(Name = Methods.Params.RefID)]
        public string RefID { get; set; }

        [DataMember(Name =  Methods.Params.Tags)]
        public string Tags
        {
            get
            {
                return tagmas.Aggregate(string.Empty, (current, tagma) => current + (tagma + ',')).TrimEnd(',');
            }
            set { AddTags(value.Split(',')); }
        }

        [DataMember(Name = Methods.Params.ItemID)]
        public int ItemID { get; set; }

        [DataMember(Name = Methods.Params.TimeUpdated)]
        protected internal double TimeUpdatedUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(TimeUpdated); }
            set { TimeUpdated = Helper.UnixTimeStampToDateTime(value); }
        }

        public DateTime TimeUpdated { get; set; }

        [DataMember(Name = Methods.Params.TimeAdded)]
        protected internal double TimeAddedUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(TimeAdded); }
            set { TimeAdded = Helper.UnixTimeStampToDateTime(value); }
        }

        public DateTime TimeAdded { get; set; }

        [DataMember(Name = Methods.Params.State)]
        public int State { get; set; }

        public Item (string url, string title = null, string refID = null)
        {
            Url = url;
            Title = title;
            RefID = refID;
            tagmas = new List<string>();
        }

        public void AddTags(params string[] tags2add)
        {
            foreach (string tag in tags2add)
            {
              tagmas.Add(tag);   
            }
        }
    }
}
