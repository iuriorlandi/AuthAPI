using AuthAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDBContext _dbContext;

        public UserRepository(AuthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteUser(string username)
        {
            var storedUser = GetUserByUsername(username);

            _dbContext.Users.Remove(storedUser);
            _dbContext.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.AsNoTracking()
                .Where(u => u.Username.Equals(username))
                .FirstOrDefault();
        }

        public void RegisterUser(User user)
        {
            var userAdded = _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdatePassword(User user)
        {
            var storedUser = GetUserByUsername(user.Username);
            storedUser.PasswordHash = user.PasswordHash;
            storedUser.Salt = user.Salt;

            _dbContext.Users.Update(storedUser);
            _dbContext.SaveChanges();
        }

        public bool UsernameExists(string username)
            => _dbContext.Users.Any(u => u.Username == username);
    }
}
