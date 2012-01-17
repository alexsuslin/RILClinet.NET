using RestSharp;

namespace RIL
{
    internal struct Methods
    {
        public const string AddNewPage = "add";
        public const string SendChangesToList = "send";
        public const string RetreiveUserStats = "stats";
        public const string RetreiveUserList = "get";
        public const string Authentication = "auth";
        public const string RegisterUser = "signup";
        public const string GetTextOnlyVersion = "text";
        public const string ApiStatus = "api";

        public struct Params
        {
            public const string Images = "images";
            public const string Mode = "mode";
            public const string Less = "less";
            public const string More = "more";
            public const string MyAppOnly = "myAppOnly";
            public const string Complete = "complete";
            public const string TimeUpdated = "time_updated";
            public const string TimeAdded = "time_added";
            public const string State = "state";
            public const string Count = "count";
            public const string Page = "page";
            public const string ItemID = "item_id";
            public const string List = "list";
            public const string Since = "since";
            public const string Status = "status";
            public const string Username = "username";
            public const string Password = "password";
            public const string ApiKey = "apikey";
            public const string Url = "url";
            public const string Title = "title";
            public const string RefID = "ref_id";
            public const string Tags = "tags";
            public const string New = "new";
            public const string Read = "read";
            public const string Unread = "unread";
            public const string UpdateTitles = "update_title";
            public const string UpdateTags = "update_tags";
            public const string JsonFormat = "json";
            public const string XmlFormat = "xml";
            public const string Format = "format";

            public const string UserSince = "user_since";
            public const string CountItems = "count_list";
            public const string CountReadItems = "count_read";
            public const string CountUnreadItems = "count_unread";

        }
    }
}
