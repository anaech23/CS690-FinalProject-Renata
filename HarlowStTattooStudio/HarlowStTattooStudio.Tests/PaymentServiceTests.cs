using Xunit;
using HarlowStTattooStudio.Services;
using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;

namespace HarlowStTattooStudio.Tests
{
    public class PaymentServiceTests
    {
        [Fact]

        public void RecordDeposit_WorksCorrectly()
        {
            // 1. Setup
            var data = new StudioData();
            var service = new PaymentService(data);
            data.Appointments.Add(new Appointment { AppointmentId = 1 });

            // 2. Do the action
            bool result = service.RecordDeposit(1, 50.00m);

            // 3. Check
            Assert.True(result);
            Assert.Equal(ChargeType.Deposit, data.Payments.First().ChargeType);
            Assert.Equal(50.00m, data.Payments.First().Amount);
        }

        [Fact]
        public void RecordDeposit_FailsAppointmentMissing()
        {
            // 1. Setup
            var data = new StudioData();
            var service = new PaymentService(data);

            // 2. Do the action
            bool result = service.RecordDeposit(999, 50.00m);

            // 3. Check
            Assert.False(result);
        }
    }
}