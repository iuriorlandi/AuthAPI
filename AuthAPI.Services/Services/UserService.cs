using AuthAPI.Data;
using AuthAPI.Data.Entities;
using AuthAPI.Services.Models;
using AuthAPI.Utilities;

namespace AuthAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ServiceResult<User> RegisterUser(UserRegistrationModel user)
        {
            byte[] passwordHash;
            byte[] salt;

            PasswordUtility.CreatePasswordHash(user.Password, out passwordHash, out salt);

            var userEntity = _userRepository.RegisterUser(new User() { Username = user.Username, Email = user.Email, PasswordHash = passwordHash, Salt = salt });
            return ServiceResult<User>.CreateSucess(userEntity);
        }
    }
}
