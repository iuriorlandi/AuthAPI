using AuthAPI.Data.Entities;
using AuthAPI.Services.Models;

namespace AuthAPI.Services
{
    public interface IUserService
    {
        ServiceResult<User> RegisterUser(UserRegistrationModel user);
    }
}
