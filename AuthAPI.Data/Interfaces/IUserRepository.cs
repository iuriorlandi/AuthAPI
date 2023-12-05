using AuthAPI.Data.Entities;

namespace AuthAPI.Data
{
    public interface IUserRepository
    {
        void DeleteUser(string username);
        User GetUserByUsername(string username);
        void RegisterUser(User user);
        void UpdatePassword(User user);
        bool UsernameExists(string username);
    }
}
