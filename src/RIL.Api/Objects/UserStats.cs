using System;
using System.Runtime.Serialization;
using RIL.Constants;

namespace RIL.Objects
{
    [DataContract]
    public class UserStats
    {
        #region Properties

        [DataMember(Name = Methods.Params.UserSince)]
        protected internal double UserSinceUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(UserSince); }
            set { UserSince = Helper.UnixTimeStampToDateTime(value); }
        }

        [DataMember(Name = Methods.Params.CountUnreadItems)]
        public int NumberOfUnreadItems { get; set; }

        [DataMember(Name = Methods.Params.CountItems)]
        public int NumberOfItems { get; set; }

        [DataMember(Name = Methods.Params.CountReadItems)]
        public int NumberOfReadItems { get; set; }

        public DateTime UserSince { get; set; }

        #endregion
    }
}
