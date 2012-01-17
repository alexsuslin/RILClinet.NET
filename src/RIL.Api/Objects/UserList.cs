using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RIL.Constants;

namespace RIL.Objects
{
    [DataContract]
    public class UserList
    {
        #region Fields

        private IList<Item> list;

        #endregion

        #region Properties

        [DataMember(Name = Methods.Params.Status)]
        protected internal int StatusSince
        {
            get { return (int) Status; }
            set { Status = (UserStatus) value; }
        }

        [DataMember(Name = Methods.Params.Since)]
        protected internal double SinceUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(Since); }
            set { Since = Helper.UnixTimeStampToDateTime(value); }
        }

        [DataMember(Name = Methods.Params.List)]
        protected internal Dictionary<int, Item> Dictionary { get; set; }

        [DataMember(Name = Methods.Params.Complete)]
        public int Complete { get; set; }

        public DateTime Since { get; set; }

        public UserStatus Status { get; set; }

        #endregion
        
        #region Constructors

        public IList<Item> List
        {
            get { return list ?? (list = Helper.ConvertToList(Dictionary)); }
        }

        #endregion
    }
}
