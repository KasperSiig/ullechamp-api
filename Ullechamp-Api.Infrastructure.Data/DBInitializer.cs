using System;
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
            ctx.SaveChanges();
        }
    }
}