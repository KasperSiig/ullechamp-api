using System;
using Moq;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.ApplicationService.Impl;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;
using Xunit;

namespace UllechampTest
{
    public class UserServiceTest
    {
        
        [Fact]
        public void VerifyCreateCalenderRunsOneTime()
        {
            var mockObject = new Mock<IUserRepository>();
            IUserService service = new UserService(mockObject.Object);

            var user = new User()
            {
                Id = 1,
                Username = "Oliver1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                KDA = 1.1,
                WinLoss = 100,
                Point = 20
            };

            service.CreateUser(user);
            
            mockObject.Verify( p => p.CreateUser(user), Times.Once);
        }
    }
}