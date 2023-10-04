using AuthAPI.Data.Entities;

namespace AuthAPI.Data
{
    public interface IUserRepository
    {
        User RegisterUser(User user);
        bool UsernameExists(string username);
    }
}
