using System;
using System.Runtime.Serialization;

namespace RIL
{
    public enum Format
    {
        Json,
        Xml
    }

    public enum State
    {
        Read,
        Unread,
        All
    }

    public enum RetreiveType
    {
        MyApp = 0,
        All = 1
    }

    public enum RetriveTags
    {
        Yes,
        No
    }

    [DataContract]
    public class UserListOptions
    {
        public Format Format { get; set; }
        
        [DataMember(Name = Methods.Params.Format)]
        protected internal string FormatParameter
        {
            get { return Format == Format.Xml ? Methods.Params.XmlFormat : Methods.Params.JsonFormat; }
        }

        public State State { get; set; }
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
        
        public RetreiveType RetreiveType { get; set; }

        [DataMember(Name = Methods.Params.MyAppOnly)]
        public int RetreiveTypeParameter { get { return RetreiveType == RetreiveType.MyApp ? 0 : 1; } }

        [DataMember(Name = Methods.Params.Since)]
        protected internal double SinceUnixFormat
        {
            get { return Helper.DateTimeToUnixTimeSpan(Since); }
            set { Since = Helper.UnixTimeStampToDateTime(value); }
        }

        public DateTime Since { get; set; }

        [DataMember(Name = Methods.Params.Count)]
        public int Count { get; set; }
        [DataMember(Name = Methods.Params.Page)]
        public int Page { get; set; }

        [DataMember(Name = Methods.Params.Tags)]
        public RetriveTags RetriveTags { get; set; }

    }
}
