using AuthAPI.Data;
using AuthAPI.Data.Entities;
using AuthAPI.Services;
using AuthAPI.Services.Models;
using AuthAPI.Utilities;
using Moq;
using System.Security.Claims;

namespace AuthAPI.Tests
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _authService = new AuthService(_userRepositoryMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public void Login_ValidPassword_ReturnSuccess()
        {
            //Arrange
            var userToLogin = new LoginModel
            {
                Username = "username",
                Password = "password"
            };

            PasswordUtility.CreatePasswordHash(userToLogin.Password, out byte[] passwordHash, out byte[] salt);
            var userOnDb = new User
            {
                PasswordHash = passwordHash,
                Salt = salt,
                UserId = 1,
                Email = "",
                Username = ""

            };

            _userRepositoryMock
                .Setup(a => a.GetUserByUsername(userToLogin.Username))
                .Returns(userOnDb);

            //Act
            var result = _authService.Login(userToLogin);

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Login_InvalidPassword_ReturnSuccessFalse()
        {
            //Arrange
            var userToLogin = new LoginModel
            {
                Username = "username",
                Password = "password"
            };

            _userRepositoryMock
                .Setup(a => a.GetUserByUsername(userToLogin.Username))
                .Returns(It.IsAny<User>());

            //Act
            var result = _authService.Login(userToLogin);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void ValidateToken_InvalidToken_ReturnSuccessFalse()
        {
            //Arrange
            var invalidToken = "invalidToken";

            _tokenServiceMock
                .Setup(a => a.ValidateToken(invalidToken))
                .Returns(ServiceResult<ClaimsPrincipal>.CreateFailure());

            //Act
            var result = _authService.ValidateToken(invalidToken);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void ValidateToken_ValidToken_ReturnSuccess()
        {
            //Arrange
            var validToken = "validToken";
            var expectedUserId = 1;
            var expectedUserName = "username";
            var expectedEmail = "user@email.com";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, expectedUserId.ToString()),
                new Claim(ClaimTypes.Name, expectedUserName.ToString()),
                new Claim(ClaimTypes.Email, expectedEmail.ToString())
            };

            _tokenServiceMock
                .Setup(a => a.ValidateToken(validToken))
                .Returns(ServiceResult<ClaimsPrincipal>
                    .CreateSuccess(new ClaimsPrincipal(new ClaimsIdentity(claims))));

            //Act
            var result = _authService.ValidateToken(validToken);

            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedUserId, result.Data.Id);
            Assert.Equal(expectedUserName, result.Data.UserName);
            Assert.Equal(expectedEmail, result.Data.Email);
        }
    }
}
