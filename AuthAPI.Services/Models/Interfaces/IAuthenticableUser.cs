namespace AuthAPI.Services.Models
{
    public interface IAuthenticableUser
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
