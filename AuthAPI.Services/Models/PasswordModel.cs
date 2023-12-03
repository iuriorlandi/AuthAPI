namespace AuthAPI.Services.Models
{
    public class PasswordModel
    {

        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public PasswordModel(string password, byte[] passwordHash, byte[] salt)
        {
            Password = password;
            PasswordHash = passwordHash;
            Salt = salt;
        }
    }
}
