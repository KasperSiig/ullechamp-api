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
        private Mock<ICalenderRepository> CreateNewMoqRepository()
        {
            //Setup mock repository
            var repository = new Mock<ICalenderRepository>();
            repository.Setup(r => r.ReadCalender())
                .Returns(SampleCalenderItems());
            
            return repository;
        }
        
        private List<CalenderItem> SampleCalenderItems()
        {
            return new List<CalenderItem>()
            {
                new CalenderItem() {Id = 1, NextEvent = new DateTime(18, 12, 05)}
            };
        }

        /// <summary>
        /// Checks setup is working
        /// </summary>
        [Fact]
        private void CalenderTestSetup()
        {
            var repository = CreateNewMoqRepository().Object;

            var expected = 1;
            var actual = repository.ReadCalender().Count();
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VerifyCreateCalenderRunsOneTime()
        {
            var mockRepo = new Mock<ICalenderRepository>();
            var service = new CalenderService(mockRepo.Object);

            var item = new CalenderItem()
            {
                Id = 1,
                NextEvent = new DateTime(18, 12, 05)
            };

            service.CreateCalender(item);

            mockRepo.Verify(p => p.CreateCalender(item), Times.Once);
        }

        [Fact]
        public void VerifyGetAllCalendersRunsOneTime()
        {
            var mockRepo = new Mock<ICalenderRepository>();
            var service = new CalenderService(mockRepo.Object);

            var item = new CalenderItem()
            {
                Id = 1,
                NextEvent = new DateTime(18, 12, 05)
            };

            service.GetCalenders();

            mockRepo.Verify(p => p.ReadCalender(), Times.Once);
        }
        
        [Fact]
        private void TestGetAllCalendersExpectNoException()
        {
            var repository = CreateNewMoqRepository();
            ICalenderService service = new CalenderService(repository.Object);

            var expected = 1;
            var actual = service.GetCalenders().Count();

            Assert.Equal(expected, actual);
        }

    }
}