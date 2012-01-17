namespace RIL
{
    public class PageContent
    {
        public string Content { get; private set; }
        public string PossibleTitle { get; private set; }
        public string LoginFound { get; private set; }
        public string NextPageUrl { get; private set; }
        public string ContentType { get; private set; }


        internal PageContent(RILResponse execute)
        {
            Content = execute.Response.Content;
            PossibleTitle = execute.Response.Headers.GetValueByName(Header.Title);
            LoginFound = execute.Response.Headers.GetValueByName(Header.LoginFound);
            NextPageUrl = execute.Response.Headers.GetValueByName(Header.NextPageUrl);
            ContentType = execute.Response.Headers.GetValueByName(Header.ContentType);
        }
    }
}
