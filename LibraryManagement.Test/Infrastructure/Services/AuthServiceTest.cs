using FluentAssertions;
using LibraryManagement.Infrastructure.Services;
using LibraryManagement.Test.Fixtures;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Test.Infrastructure.Services
{
    public class AuthServiceTest
    {
        private static readonly string _key = "KeyKeyKeyKeyKeyKeyKeyKeyKeyKeyKeyKeyKeyKey";
        private static readonly string _issuer = "Issuer";
        private static readonly string _audience = "Audience";
        private static readonly string _seconds = "1200";

        private static readonly Dictionary<string, string> _inMemorySettings = new()
        {
            { "AuthSettings:Key", _key },
            { "AuthSettings:Issuer", _issuer },
            { "AuthSettings:Audience", _audience },
            { "AuthSettings:ExpirationSeconds", _seconds }
        };

        private readonly IConfiguration _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(_inMemorySettings!)
                .Build();

        [Fact]
        public void Should_Generate_Sha256_Hash()
        {
            // Arrange
            var password = "P@assword0123456789";

            var service = new AuthService(_configuration);

            // Act
            var result = service.GenerateSha256Hash(password);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().Be("f82674478890925c07f5446c50065c2387a12a7dc27adcd7eea5c6ec4f1c1ac9");
        }

        [Fact]
        public void Should_Generate_JwtToken()
        {
            // Arrange
            var user = UserDataGenerator.CreateUserCommandFake();

            var service = new AuthService(_configuration);

            // Act
            var result = service.GenerateJwtToken(user.Name, user.Role);

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}
