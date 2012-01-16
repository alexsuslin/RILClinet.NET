using System;
using System.Runtime.Serialization;

namespace RIL
{
    [DataContract]
    internal class UserStatsWrapper
    {
        [DataMember(Name = Methods.Params.UserSince)]
        public string UserSince { get; set; }
        [DataMember(Name = Methods.Params.CountItems)]
        public string NumberOfItems { get; set; }
        [DataMember(Name = Methods.Params.CountUnreadItems)]
        public string NumberOfUnreadItems { get; set; }
        [DataMember(Name = Methods.Params.CountReadItems)]
        public string NumberOfReadItems { get; set; }
    }

    public class UserStats
    {
        private readonly DateTime? userSince;
        private readonly int? numberOfItems;
        private readonly int? numberOfUnreadItems;
        private readonly int? numberOfReadItems;

        public DateTime? UserSince
        {
            get { return userSince; }
        }

        public int? NumberOfUnreadItems
        {
            get { return numberOfUnreadItems; }
        }

        public int? NumberOfItems
        {
            get { return numberOfItems; }
        }

        public int? NumberOfReadItems
        {
            get { return numberOfReadItems; }
        }

        internal UserStats(string userSince, string numberOfItems, string numberOfUnreadItems, string numberOfReadItems)
        {
            double dt;
            this.userSince = double.TryParse(userSince, out dt) ? Helper.UnixTimeStampToDateTime(dt) : (DateTime?)null;

            int placeHolder;
            this.numberOfItems = int.TryParse(numberOfItems, out placeHolder) ? placeHolder : (int?) null;
            this.numberOfUnreadItems = int.TryParse(numberOfUnreadItems, out placeHolder) ? placeHolder : (int?)null;
            this.numberOfReadItems = int.TryParse(numberOfReadItems, out placeHolder) ? placeHolder : (int?)null;
        }
    }
}
