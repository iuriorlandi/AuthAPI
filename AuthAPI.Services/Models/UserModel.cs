namespace AuthAPI.Services.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public UserModel(int id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
        }

    }
}
