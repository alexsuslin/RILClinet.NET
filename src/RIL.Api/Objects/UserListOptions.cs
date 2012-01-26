using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using RIL.Constants;
using RIL.Objects.JsonUtilities;

namespace RIL.Objects
{
    [DataContract]
    public class UserListOptions
    {
        #region Properties

        [DataMember(Name = Methods.Params.State)]
        protected internal string StateParameter
        {
            get
            {
                if (State == State.Read)
                    return Methods.Params.Read;
                if (State == State.Unread)
                    return Methods.Params.Unread;
                return string.Empty;
            }
        }

        [DataMember(Name = Methods.Params.Since)]
        protected internal double SinceUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(Since); }
            set { Since = Helper.UnixTimeStampToDateTime(value); }
        }

        [DataMember(Name = Methods.Params.MyAppOnly)]
        protected internal int RetreiveTypeParameter
        {
            get { return RetreiveType == RetreiveType.MyApp ? 0 : 1; }
        }

        [DataMember(Name = Methods.Params.Count)]
        public int Count { get; set; }

        [DataMember(Name = Methods.Params.Page)]
        public int Page { get; set; }

        [DataMember(Name = Methods.Params.Tags)]
        protected internal int retrieveTags { get; set; }

        [DataMember(Name = Methods.Params.Format), JsonConverter(typeof(ApiEnumConverter))]
        public Format Format { get; set; }

        public DateTime Since { get; set; }

        public State State { get; set; }

        public RetreiveType RetreiveType { get; set; }

        public RetrieveTags RetrieveTags { get { return (RetrieveTags)retrieveTags; } set { retrieveTags = (int) value; } }

        #endregion

    }
}
