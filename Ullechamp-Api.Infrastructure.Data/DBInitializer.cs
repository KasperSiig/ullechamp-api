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
                Twitchname = "Oliver1992",
                Role = "Standard",
                Wins = 26,
                Losses = 14,
                Kills = 0,
                Deaths = 0,
                Assists = 0,
                Kda = 0,
                WinLoss = 0,
                Point = 0
            }).Entity;

            var jesper = ctx.Users.Add(new User()
            {
                Twitchname = "Jesper1992",
                Role = "Standard",
                Wins = 0,
                Losses = 17,
                Kills = 0,
                Deaths = 0,
                Assists = 0,
                Kda = 0,
                WinLoss = 0,
                Point = 0
            }).Entity;
            
            var kasper = ctx.Users.Add(new User()
            {
                Twitchname = "Kasper1992",
                Role = "Standard",
                Wins = 0,
                Losses = 16,
                Kills = 0,
                Deaths = 0,
                Assists = 0,
                Kda = 0,
                WinLoss = 0,
                Point = 0
            }).Entity;
            
            var kasper2 = ctx.Users.Add(new User()
            {
                Twitchname = "Kasper1992",
                Role = "Standard",
                Wins = 0,
                Losses = 6,
                Kills = 0,
                Deaths = 0,
                Assists = 0,
                Kda = 0,
                WinLoss = 0,
                Point = 0
            }).Entity;
            
            var kasper3 = ctx.Users.Add(new User()
            {
                Twitchname = "Kasper1992",
                Role = "Standard",
                Wins = 0,
                Losses = 18,
                Kills = 0,
                Deaths = 0,
                Assists = 0,
                Kda = 0,
                WinLoss = 0,
                Point = 0
            }).Entity;
            
            var kasperOG = ctx.Users.Add(new User()
            {
                TwitchId = 110649572,
                Twitchname = "kaspersiig",
                Role = "Admin",
                Wins = 0,
                Losses = 0,
                Kills = 0,
                Deaths = 0,
                Assists = 0,
                Kda = 0,
                WinLoss = 0,
                Point = 0,
                ImageUrl = "https://static-cdn.jtvnw.net/user-default-pictures/4cbf10f1-bb9f-4f57-90e1-15bf06cfe6f5-profile_image-300x300.jpg"
            }).Entity;
            ctx.SaveChanges();
        }
    }
}