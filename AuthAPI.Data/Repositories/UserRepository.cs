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

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.AsNoTracking()
                .Where(u => u.Username.Equals(username))
                .FirstOrDefault();
        }

        public User RegisterUser(User user)
        {
            var userAdded = _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return userAdded.Entity;
        }

        public bool UsernameExists(string username)
            => _dbContext.Users.Any(u => u.Username == username);
    }
}
