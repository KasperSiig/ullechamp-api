using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;
using Moq;
using Ullechamp_Api.Core.ApplicationService.Impl;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Infrastructure.Data.Repositories;
using Xunit;

namespace UllechampTest
{
    public class CalenderServiceTest
    {
        private ICalenderRepository GetMockCalender()
        {
            Mock<ICalenderRepository> mockObject = new Mock<ICalenderRepository>();
            return mockObject.Object;
        }
        
        [Fact]
        public void GetAllCalenders()
        {
            ICalenderRepository test = this.GetMockCalender();
            ICalenderService calenderService = new CalenderService(test);

            var actualResult = calenderService.CreateCalender(new CalenderItem()
            {
                Id = 1,
                NextEvent = new DateTime(18, 12, 04, 19, 0, 0)
            });
            Assert.NotNull(calenderService.GetCalenders());
        }
    }
}