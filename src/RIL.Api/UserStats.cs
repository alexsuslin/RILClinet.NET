using System;
using System.Runtime.Serialization;

namespace RIL
{
    [DataContract]
    public class UserStats
    {
        [DataMember(Name = Methods.Params.UserSince)]
        public double UserSinceUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(UserSince); }
            set { UserSince = Helper.UnixTimeStampToDateTime(value); }
        }

        public DateTime UserSince { get; set; }

        [DataMember(Name = Methods.Params.CountUnreadItems)]
        public int NumberOfUnreadItems { get; set; }

        [DataMember(Name = Methods.Params.CountItems)]
        public int NumberOfItems { get; set; }

        [DataMember(Name = Methods.Params.CountReadItems)]
        public int NumberOfReadItems { get; set; }
    }
}
