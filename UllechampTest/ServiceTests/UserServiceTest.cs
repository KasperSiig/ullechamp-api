using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private Mock<IUserRepository> CreateNewMoqRepository()
        {
            var repository = new Mock<IUserRepository>();
            repository.Setup(r => r.ReadAllUsers())
                .Returns(SampleUsers());
            repository.Setup(x => x.ReadById(2)).Returns(SampleUsers()[1]);
            
            return repository;
        }
        
        private List<User> SampleUsers()
        {
            return new List<User>()
            {
                new User() {Id = 1,
                Twitchname = "Oliver1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                Kda = 1.1,
                WinLoss = 100,
                Point = 20 }, 
                
                new User() {
                Id = 2,
                Twitchname = "Oliver1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                Kda = 1.1,
                WinLoss = 100,
                Point = 20 }
            };
        }
        
        [Fact]
        public void VerifyCreateUserRunsOneTime()
        {
            var mockObject = new Mock<IUserRepository>();
            IUserService service = new UserService(mockObject.Object);

            var user = new User()
            {
                Id = 1,
                Twitchname = "Oliver1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                Kda = 1.1,
                WinLoss = 100,
                Point = 20
            };

            service.CreateUser(user);
            
            mockObject.Verify( p => p.CreateUser(user), Times.Once);
        }

        [Fact]
        private void VerifyDeleteUserRunsOneTime()
        {
            var mockObject = new Mock<IUserRepository>();
            IUserService service = new UserService(mockObject.Object);
            
            var user = new User()
            {
                Id = 1,
                Twitchname = "Oliver1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                Kda = 1.1,
                WinLoss = 100,
                Point = 20
            };

            service.Delete(user.Id);
            
            mockObject.Verify(x => x.Delete(user.Id), Times.Once);
        }

        [Fact]
        private void VerifyUpdateUserRunsOneTime()
        {
            var mockObject = new Mock<IUserRepository>();
            IUserService service = new UserService(mockObject.Object);
            
            var user = new User()
            {
                Id = 1,
                Twitchname = "Oliver1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                Kda = 1.1,
                WinLoss = 100,
                Point = 20
            };

            service.Update(user);
            
            mockObject.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }
        
        [Fact]
        private void TestGetAllUserCountNoException()
        {
            var repository = CreateNewMoqRepository();
            IUserService service = new UserService(repository.Object);

            var expected = 2;
            var actual = service.GetAllUsers().Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 2)]
        private void GetUserById(int id, int expected)
        {
            var repository = CreateNewMoqRepository();
            IUserService service = new UserService(repository.Object);
            
            var actual = service.GetById(id).Id;
            
            Assert.Equal(expected, actual);
        }
    }
}