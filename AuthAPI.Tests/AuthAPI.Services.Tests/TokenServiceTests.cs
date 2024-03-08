using AuthAPI.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthAPI.Tests
{
    public class TokenServiceTests
    {
        private readonly TokenService _tokenService;
        private readonly Mock<IConfiguration> _configurationMock;

        public TokenServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _tokenService = new TokenService(_configurationMock.Object);
        }

        [Fact]
        public void GenerateToken_ReturnsValidToken()
        {
            //Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "test_user"),
                new Claim(ClaimTypes.Email, "user@email.com")
            };

            var secretKey = Guid.NewGuid().ToString();
            var issuer = "Issuer";
            var audience = "Audience";

            _configurationMock.Setup(x => x["Jwt:SecretKey"]).Returns(secretKey);
            _configurationMock.Setup(x => x["Jwt:ExpirationDays"]).Returns("1");
            _configurationMock.Setup(x => x["Jwt:Issuer"]).Returns(issuer);
            _configurationMock.Setup(x => x["Jwt:Audience"]).Returns(audience);

            //Act
            var tokenGenerated = _tokenService.GenerateToken(claims);

            //Assert
            Assert.False(string.IsNullOrWhiteSpace(tokenGenerated));

            var handler = new JwtSecurityTokenHandler();
            var jwtToken  = handler.ReadToken(tokenGenerated) as JwtSecurityToken;

            Assert.NotNull(jwtToken);
            Assert.Equal(issuer, jwtToken.Issuer);
            Assert.Equal(audience, jwtToken.Audiences.FirstOrDefault());
        }

        [Fact]
        public void ValidateToken_ValidToken_ReturnsSuccess()
        {
            //Arrange
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "test_user"),
                new Claim(ClaimTypes.Email, "user@email.com")
            };

            var secretKey = Guid.NewGuid().ToString();
            var issuer = "Issuer";
            var audience = "Audience";

            _configurationMock.Setup(x => x["Jwt:SecretKey"]).Returns(secretKey);
            _configurationMock.Setup(x => x["Jwt:ExpirationDays"]).Returns("1");
            _configurationMock.Setup(x => x["Jwt:Issuer"]).Returns(issuer);
            _configurationMock.Setup(x => x["Jwt:Audience"]).Returns(audience);

            var validToken = _tokenService.GenerateToken(claims);
            
            //Act
            var result = _tokenService.ValidateToken(validToken);

            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Identity.IsAuthenticated);
        }
    }
}
