namespace RIL
{
    /// <summary>
    /// Each call to the API will return a number of informational headers. The most important 
    /// of which is the status header which indicates if the call was successful or not.
    /// </summary>
    internal struct Header
    {
        #region Only for Text Method

        /// <summary>
        /// This is the title of the page as detected by RIL.
        /// </summary>
        public const string Title = "X-Title";

        /// <summary>
        /// It looks like there may be a login form on this page. If the user needs to login to view 
        /// the content and you have access to their cookies you may want to provide a fallback generator.
        /// </summary>
        public const string LoginFound = "X-Login-Found";
        
        /// <summary>
        ///  If the page appears to be a multi-page article (ex: had a 'next page' link), 
        ///  this will return the url of the next page.
        /// </summary>
        public const string NextPageUrl = "X-Next";

        /// <summary>
        /// It's important to use the correct character set when displaying the content to the user.  Make use of the 
        /// information in the Content-Type, for example applying it to your Content-Type meta tag in the head of your document.
        /// </summary>
        public const string ContentType = "Content-Type";
       
        #endregion

        /// <summary>
        /// Indicates if the call was successful or not
        /// </summary>
        public const string Status = "Status";
        /// <summary>
        /// More detailed description of the error. Also in the case of a 503 status (when RIL 
        /// is undergoing maintenance, this will provide information to display to the user. 
        /// Ex: 'Read It Later is upgrading its servers and will return in 30 minutes')
        /// </summary>
        public const string Error = "X-Error";

        /// <summary>
        /// Current rate limit enforced per user
        /// </summary>
        public const string UserLimit = "X-Limit-User-Limit";

        /// <summary>
        /// Number of calls remaining before hitting user's rate limit
        /// </summary>
        public const string UserLimitRemaining = "X-Limit-User-Remaining";

        /// <summary>
        /// Seconds until user's rate limit resets
        /// </summary>
        public const string UserLimitReset = "X-Limit-User-Reset";

        /// <summary>
        /// Current rate limit enforced per API key
        /// </summary>
        public const string KeyLimit = "X-Limit-Key-Limit";

        /// <summary>
        /// Number of calls remaining before hitting API key's rate limit
        /// </summary>
        public const string KeyLimitRemaining = "X-Limit-Key-Remaining";

        /// <summary>
        /// Seconds until API key rate limit resets
        /// </summary>
        public const string KeyLimitReset = "X-Limit-Key-Reset";
    }
}
