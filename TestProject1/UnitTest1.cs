using JwtApý.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TokenGenerator_ReturnsToken_WhenUserValid()
        {
            var mock = new Mock<IConfiguration>();
            mock.SetupGet(c => c[It.IsAny<string>()]).Returns("unlucounlucounluco");

            //arrange
            var tokenService = new TokenService(mock.Object);
            //act
            var token = tokenService.TokenGenerator(new JwtApý.Models.User { Name = "Taha" });
            //assert
            Assert.NotNull(token);
            Assert.True(DateTime.Now < token.Expiration);
            Assert.NotEmpty(token.AccessToken);
            Assert.NotEmpty(token.RefreshToken);

        }
    }
}