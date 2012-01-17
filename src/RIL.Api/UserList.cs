using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RIL
{
    public enum UserStatus
    {
        Normal = 0,
        NoChange = 1
    }

    [DataContract]
    public class UserList
    {
        [DataMember(Name = Methods.Params.Status)]
        protected internal int StatusSince
        {
            get { return (int) Status; }
            set { Status = (UserStatus) value; }
        }

        public UserStatus Status { get; set; }

        [DataMember(Name = Methods.Params.Complete)]
        public int Complete { get; set; }

        [DataMember(Name = Methods.Params.Since)]
        protected internal double SinceUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(Since); }
            set { Since = Helper.UnixTimeStampToDateTime(value); }
        }

        public DateTime Since { get; set; }


        [DataMember(Name = Methods.Params.List)]
        protected internal Dictionary<int, Item> Dictionary { get; set; }

        private IList<Item> list; 
        public IList<Item> List
        {
            get { return list ?? (list = Helper.ConvertToList(Dictionary)); }
        }
    }
}
