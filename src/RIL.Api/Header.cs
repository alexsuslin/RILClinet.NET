namespace RIL
{
    /// <summary>
    /// Each call to the API will return a number of informational headers. The most important 
    /// of which is the status header which indicates if the call was successful or not.
    /// </summary>
    internal struct Header
    {
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
