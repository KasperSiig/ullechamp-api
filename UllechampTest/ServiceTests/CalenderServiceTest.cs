using System;
using System.Collections.Generic;
using System.Linq;
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
        private static Mock<ICalenderRepository> CreateNewMoqRepository()
        {
            var repository = new Mock<ICalenderRepository>();
            repository.Setup(r => r.ReadCalender())
                .Returns(SampleCalenderItems());
            
            return repository;
        }
        
        private static List<CalenderItem> SampleCalenderItems()
        {
            return new List<CalenderItem>()
            {
                new CalenderItem() {Id = 1, NextEvent = new DateTime(18, 12, 05)}
            };
        }

        [Fact]
        private void CalenderTestSetup()
        {
            var repository = CreateNewMoqRepository();

            var all = repository.Object.ReadCalender().Count();
            
            Assert.Equal(1, all);
        }

        [Fact]
        public void VerifyCreateCalenderRunsOneTime()
        {
            var mockObject = new Mock<ICalenderRepository>();
            var service = new CalenderService(mockObject.Object);

            var item = new CalenderItem()
            {
                Id = 1,
                NextEvent = new DateTime(18, 12, 05)
            };

            service.CreateCalender(item);

            mockObject.Verify(p => p.CreateCalender(item), Times.Once);
        }

        [Fact]
        public void VerifyGetAllCalendersRunsOneTime()
        {
            var mockObject = new Mock<ICalenderRepository>();
            var service = new CalenderService(mockObject.Object);

            var item = new CalenderItem()
            {
                Id = 1,
                NextEvent = new DateTime(18, 12, 05)
            };

            service.GetCalenders();

            mockObject.Verify(p => p.ReadCalender(), Times.Once);
        }
        
        [Fact]
        private void GetAllCalenders()
        {
            var repository = CreateNewMoqRepository();
            ICalenderService service = new CalenderService(repository.Object);

            int expectedResult = 1;

            var countOfCalenders = service.GetCalenders().Count();

            Assert.Equal(expectedResult, countOfCalenders);
        }

    }
}