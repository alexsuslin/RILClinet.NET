namespace RIL
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
