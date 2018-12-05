using System;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(UllechampContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            
            var calender1 = ctx.Calenders.Add(new CalenderItem()
                {
                    NextEvent = new DateTime(2018, 12, 04)
                });

            var user1 = ctx.Users.Add(new User()
            {
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
            });

            var user2 = ctx.Users.Add(new User()
            {
                Username = "Jesper1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                KDA = 1.1,
                WinLoss = 100,
                Point = 50
            });
            
            var user3 = ctx.Users.Add(new User()
            {
                Username = "Kasper1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                KDA = 1.1,
                WinLoss = 100,
                Point = 1
            });
            ctx.SaveChanges();
        }
    }
}