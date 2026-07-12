using Xunit;
using System;
using System.Linq;
using HarlowStTattooStudio.Services;
using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;

namespace HarlowStTattooStudio.Tests
{
    public class ReportServiceTests
    {
        [Fact]
        public void GetRevenueByDateRange_ReturnsCorrect()
        {
            // 1. Setup
            var data = new StudioData();
            var service = new ReportService(data);

            // 2. Setting specific date range
            var start = new DateTime(2026, 07, 01);
            var end = new DateTime(2026, 07, 31);

            data.Payments.Add(new Payment { Amount = 100, IsConfirmed = true, PaymentDate = new DateTime(2026, 07, 19) });
            data.Payments.Add(new Payment { Amount = 50, IsConfirmed = true, PaymentDate = new DateTime(2026, 08, 01) });

            // 3. Do the action
            var result = service.GetRevenueByDateRange(start, end);

            // 4. Check
            Assert.Equal(100, result);
        }
    }
}