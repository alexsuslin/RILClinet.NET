namespace RIL.Api.Tests
{
    using AppConfig = System.Configuration.ConfigurationManager;

    internal static class Config
    {
        #region Properties

        internal static string ApiKey = AppConfig.AppSettings["ApiKey"] ?? "<INSERT API KEY>";
        internal static string Username = AppConfig.AppSettings["Username"] ?? "<INSERT USERNAME>";
        internal static string Password = AppConfig.AppSettings["Password"] ?? "<INSERT PASSWORD>";

        #endregion
    }

}
