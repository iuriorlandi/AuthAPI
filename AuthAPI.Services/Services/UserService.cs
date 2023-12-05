using AuthAPI.Data;
using AuthAPI.Data.Entities;
using AuthAPI.Services.Models;
using AuthAPI.Utilities;

namespace AuthAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public ServiceResult<User> AlterPassword(AlterPasswordModel user)
        {

            var auth = _authService.Login(user);
            if (!auth.Success)
                return ServiceResult<User>.CreateFailure(auth.Errors.ToArray());

            var passwordModel = CreatePasswordModel(user.NewPassword);

            _userRepository.UpdatePassword(new User { Username = user.Username, PasswordHash = passwordModel.PasswordHash, Salt = passwordModel.Salt});

            return ServiceResult<User>.CreateSuccess();
        }


        public ServiceResult<User> DeleteUser(LoginModel user)
        {
            var auth = _authService.Login(user);
            if (!auth.Success)
                return ServiceResult<User>.CreateFailure(auth.Errors.ToArray());

            _userRepository.DeleteUser(user.Username);

            return ServiceResult<User>.CreateSuccess();
        }

        public ServiceResult<User> RegisterUser(UserRegistrationModel user)
        {
            var passwordModel = CreatePasswordModel (user.Password);

            _userRepository.RegisterUser(new User() { Username = user.Username, Email = user.Email, PasswordHash = passwordModel.PasswordHash, Salt = passwordModel.Salt });
            return ServiceResult<User>.CreateSuccess();
        }

        private PasswordModel CreatePasswordModel(string password)
        {
            byte[] passwordHash;
            byte[] salt;

            PasswordUtility.CreatePasswordHash(password, out passwordHash, out salt);

            return new PasswordModel (password, passwordHash, salt);
        }
    }
}
