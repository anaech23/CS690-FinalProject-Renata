using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using HarlowStTattooStudio.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.Tests
{
    public class LeaveServiceTests
    {
        private readonly StudioData _data;
        private readonly LeaveService _service;

        public LeaveServiceTests()
        {
            _data = new StudioData();
            _service = new LeaveService(_data);
        }

        [Fact]
        public void ScheduleLeave_ValidDates_ReturnTrue()
        {
            var leave = new Leave { ArtistId = 1, LeaveStart = new DateTime(2026, 08, 01), LeaveEnd = new DateTime(2026, 08, 05) };
            var result = _service.ScheduleLeave(leave);

            Assert.True(result);
            Assert.Single(_data.Leaves);
        }

        [Fact]
        public void ScheduleLeave_EndDateBeforeStartDate_ReturnsFalse()
        {
            var leave = new Leave { ArtistId = 1, LeaveStart = new DateTime(2026, 08, 10), LeaveEnd = new DateTime(2026, 08, 05) };
            var result = _service.ScheduleLeave(leave);

            Assert.False(result);
            Assert.Empty(_data.Leaves);
        }

        [Fact]
        public void ScheduleLeave_OverlappingDates_ReturnsFalse()
        {
            _data.Leaves.Add(new Leave { ArtistId = 1, LeaveStart = new DateTime(2026, 08, 01), LeaveEnd = new DateTime(2026, 08, 10) });

            var newLeave = new Leave { ArtistId = 1, LeaveStart = new DateTime(2026, 08, 05), LeaveEnd = new DateTime(2026, 08, 15) };
            var result = _service.ScheduleLeave(newLeave);

            Assert.False(result);
        }
    }
}