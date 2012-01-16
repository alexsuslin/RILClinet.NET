namespace RIL
{
    /// <summary>
    /// Each call to the API will return a number of informational headers. The most important 
    /// of which is the status header which indicates if the call was successful or not.
    /// 
    /// NOTE: If there was an error, look at the X-Error header for a description of the problem
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Request was successful
        /// </summary>
        Success = 200,

        /// <summary>
        /// Invalid request, please make sure you follow the documentation for proper syntax
        /// </summary>
        InvalidRequest = 400,

        /// <summary>
        /// Username and/or Password is incorrect
        /// </summary>
        IncorrectCredentials = 401,

        /// <summary>
        /// Rate limit exceeded, please wait a little bit before resubmitting
        /// </summary>
        RateLimitExceeded = 403,

        /// <summary>
        /// Read It Later's sync server is down for scheduled maintenance
        /// </summary>
        ServerIsDown = 503
    }
}
