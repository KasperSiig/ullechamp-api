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
    public class TournamentServiceTest
    {
        private Mock<ITournamentRepository> CreateNewMoqRepository()
        {
            var repository = new Mock<ITournamentRepository>();
            repository.Setup(r => r.ReadUsersInQueue()).Returns(QueueSample());
            repository.Setup(x => x.ReadUsersInCurrent()).Returns(TournamentSample());

            return repository;
        }

        private List<TournamentUser> TournamentSample()
        {
            return new List<TournamentUser>()
            {
                new TournamentUser()
                {
                    User = new User()
                    {
                        Id = 1,
                        Point = 2,
                        Wins = 2,
                        Kills = 2,
                        Losses = 2,
                        Deaths = 2,
                        Assists = 2,
                        WinLoss = 50,
                        Kda = 1,
                        Role = "Standard",
                        Rank = 4,
                        Lolname = "bølle",
                        Twitchname = "bølle2",
                        TwitchId = 1
                    },
                    Tournament = new Tournament()
                    {
                        DateTime = DateTime.Now,
                        Id = 1,
                        State = 1
                    },
                    TournamentId = 1,
                    Team = 1,
                    UserId = 1
                },
                new TournamentUser()
                {
                    User = new User()
                    {
                        Id = 2,
                        Point = 2,
                        Wins = 2,
                        Kills = 2,
                        Losses = 2,
                        Deaths = 2,
                        Assists = 2,
                        WinLoss = 50,
                        Kda = 1,
                        Role = "Standard",
                        Rank = 4,
                        Lolname = "bølle",
                        Twitchname = "bølle2",
                        TwitchId = 1
                    },
                    Tournament = new Tournament()
                    {
                        DateTime = DateTime.Now,
                        Id = 1,
                        State = 1
                    },
                    TournamentId = 2,
                    Team = 0,
                    UserId = 2
                }
            };
        }

        private List<User> QueueSample()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Point = 2,
                    Wins = 2,
                    Kills = 2,
                    Losses = 2,
                    Deaths = 2,
                    Assists = 2,
                    WinLoss = 50,
                    Kda = 1,
                    Role = "Standard",
                    Rank = 4,
                    Lolname = "bølle",
                    Twitchname = "bølle2",
                    TwitchId = 1
                },

                new User()
                {
                    Id = 1,
                    Point = 2,
                    Wins = 2,
                    Kills = 2,
                    Losses = 2,
                    Deaths = 2,
                    Assists = 2,
                    WinLoss = 50,
                    Kda = 1,
                    Role = "Standard",
                    Rank = 4,
                    Lolname = "bølle",
                    Twitchname = "bølle2",
                    TwitchId = 1
                },
            };
        }

        [Fact]
        private void TestGetAllUsersInQueueCountNoException()
        {
            var repository = CreateNewMoqRepository();
            ITournamentService service = new TournamentService(repository.Object);

            var expected = 2;
            var actual = service.GetUsersInQueue().Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        private void TestGetAllUsersInCurrentCountNoException()
        {
            var repository = CreateNewMoqRepository();
            ITournamentService service = new TournamentService(repository.Object);

            var expected = 2;
            var actual = service.GetUsersInCurrent().Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        private void VerifyDeleteQueueRunsOneTime()
        {
            var mockObject = new Mock<ITournamentRepository>();
            ITournamentService service = new TournamentService(mockObject.Object);

            var queue = new Queue()
            {
                Id = 1,
                User = new User()
                {
                    Id = 1,
                    Point = 2,
                    Wins = 2,
                    Kills = 2,
                    Losses = 2,
                    Deaths = 2,
                    Assists = 2,
                    WinLoss = 50,
                    Kda = 1,
                    Role = "Standard",
                    Rank = 4,
                    Lolname = "bølle",
                    Twitchname = "bølle2",
                    TwitchId = 1
                },
                QueueTime = DateTime.Now
            };

            service.RemoveFromQueue(queue.Id);

            mockObject.Verify(x => x.RemoveFromQueue(queue.Id), Times.Once);
        }

        [Fact]
        private void VerifyUpdateUserRunsOneTime()
        {
            var mockObject = new Mock<ITournamentRepository>();
            ITournamentService service = new TournamentService(mockObject.Object);

            var users = QueueSample();

            service.UpdateUsers(users);

            mockObject.Verify(x => x.UpdateUsers(It.IsAny<List<User>>()), Times.Once);
        }
    }
}