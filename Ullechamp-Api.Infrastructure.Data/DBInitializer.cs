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
            
            var calender1 = ctx.CalenderItems.Add(new CalenderItem()
                {
                    NextEvent = new DateTime(2018, 12, 04)
                }).Entity;

            var oliver = ctx.Users.Add(new User()
            {
                Username = "Oliver1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 2,
                Deaths = 55,
                Assists = 1,
                Kda = 1.1,
                WinLoss = 100,
                Point = 20
            }).Entity;

            var jesper = ctx.Users.Add(new User()
            {
                Username = "Jesper1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 46,
                Deaths = 55,
                Assists = 1,
                Kda = 1.1,
                WinLoss = 100,
                Point = 50
            }).Entity;
            
            var kasper = ctx.Users.Add(new User()
            {
                Username = "Kasper1992",
                Role = "Standard",
                Wins = 0,
                Losses = 5,
                Kills = 8,
                Deaths = 55,
                Assists = 1,
                Kda = 1.1,
                WinLoss = 100,
                Point = 1
            }).Entity;
            ctx.SaveChanges();
        }
    }
}