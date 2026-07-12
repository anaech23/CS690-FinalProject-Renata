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
            // Arrange
            var data = new StudioData();
            var service = new PaymentService(data);
            data.Appointments.Add(new Appointment { AppointmentId = 1 });

            // Do the action
            bool result = service.RecordDeposit(1, 50.00m);

            // Assert
            Assert.True(result);
            Assert.Equal(ChargeType.Deposit, data.Payments.First().ChargeType);
            Assert.Equal(50.00m, data.Payments.First().Amount);
        }

        [Fact]
        public void RecordDeposit_FailsAppointmentMissing()
        {
            // Arrange
            var data = new StudioData();
            var service = new PaymentService(data);

            // Act
            bool result = service.RecordDeposit(999, 50.00m);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasDeposit_ReturnsTrue_WhenDepositExists()
        {
            // Arrange
            var data = new StudioData();
            var service = new PaymentService(data);
            data.Appointments.Add(new Appointment { AppointmentId = 1 });
            service.RecordDeposit(1, 50.00m);

            // Act
            var result = service.HasDeposit(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RecordFullPayment_WorksCorrectly()
        {
            // Arrange
            var data = new StudioData();
            var service = new PaymentService(data);
            data.Appointments.Add(new Appointment { AppointmentId = 1 });

            // Act
            bool result = service.RecordFullPayment(1, 200.00m);

            // Assert
            Assert.True(result);
            Assert.Equal(ChargeType.FullPayment, data.Payments.First().ChargeType);
            Assert.Equal(200.00m, data.Payments.First().Amount);
        }
    }
}