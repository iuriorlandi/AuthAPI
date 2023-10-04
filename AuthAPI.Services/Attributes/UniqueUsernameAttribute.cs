using AuthAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Services.Attributes
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        public UniqueUsernameAttribute()
        {
            ErrorMessage = "Username already exists.";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value.ToString();
            var userRepository = validationContext.GetService(typeof(IUserRepository)) as IUserRepository;

            if (userRepository.UsernameExists(username))
                return new ValidationResult(ErrorMessage);


            return ValidationResult.Success;
        }
    }
}
