using AuthAPI.Data;
using AuthAPI.Services;
using AuthAPI.Services.Models;
using Moq;

namespace AuthAPI.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAuthService> _authService;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authService = new Mock<IAuthService>();
            _userService = new UserService(_userRepositoryMock.Object, _authService.Object);
        }

        [Fact]
        public void AlterPassword_InvalidLogin_ReturnsSuccessFalse()
        {
            //Arrange
            var userToAlterPassword = new AlterPasswordModel
            {
                Username = "username",
                Password = "password",
                NewPassword = "newPassword"
            };

            _authService
                .Setup(a => a.Login(userToAlterPassword))
                .Returns(ServiceResult<AuthenticatedUserModel>.CreateFailure());

            //Act
            var result = _userService.AlterPassword(userToAlterPassword);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void AlterPassword_ValidLogin_ReturnsSuccess()
        {
            //Arrange
            var userToAlterPassword = new AlterPasswordModel
            {
                Username = "username",
                Password = "password",
                NewPassword = "newPassword"
            };

            _authService
                .Setup(a => a.Login(userToAlterPassword))
                .Returns(ServiceResult<AuthenticatedUserModel>.CreateSuccess(new AuthenticatedUserModel()));

            //Act
            var result = _userService.AlterPassword(userToAlterPassword);

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void DeleteUser_InvalidLogin_ReturnsSuccess()
        {
            //Arrange
            var userToDelete = new LoginModel
            {
                Username = "username",
                Password = "password"
            };

            _authService
                .Setup(a => a.Login(userToDelete))
                .Returns(ServiceResult<AuthenticatedUserModel>.CreateFailure());

            //Act
            var result = _userService.DeleteUser(userToDelete);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void DeleteUser_ValidLogin_ReturnsSuccess()
        {
            //Arrange
            var userToDelete = new LoginModel
            {
                Username = "username",
                Password = "password"
            };

            _authService
                .Setup(a => a.Login(userToDelete))
                .Returns(ServiceResult<AuthenticatedUserModel>.CreateSuccess(new AuthenticatedUserModel()));

            //Act
            var result = _userService.DeleteUser(userToDelete);

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void RegisterUser_ValidUser_ReturnsSuccess()
        {
            //Arrange
            var userToRegister = new UserRegistrationModel
            {
                Username = "username",
                Password = "password",
                Email = "user@email.com"
            };

            //Act
            var result = _userService.RegisterUser(userToRegister);

            //Assert
            Assert.True(result.Success);
        }
    }
}