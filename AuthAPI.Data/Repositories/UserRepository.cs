using AuthAPI.Data.Entities;

namespace AuthAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDBContext _dbContext;

        public UserRepository(AuthDBContext dbContext)
        {
            _dbContext = dbContext;
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
