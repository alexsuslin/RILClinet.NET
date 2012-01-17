namespace RIL.Objects
{
    public class Credentials
    {
        #region Properties

        public string Username { get; set; }
        public string Password { get; set; }

        #endregion

        #region Constructors

        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        #endregion
    }
}
