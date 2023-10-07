using AuthAPI.Data.Entities;

namespace AuthAPI.Data
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        User RegisterUser(User user);
        bool UsernameExists(string username);
    }
}
