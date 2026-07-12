using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using HarlowStTattooStudio.Services;
using Xunit;


namespace HarlowStTattooStudio.Tests
{
    public class AppointmentServiceTests
    {
        [Fact]
        public void ScheduleAppointment_ShouldReturnFalse_WhenArtistIsOnLeave()
        {
            // Arrange: Create the fake data
            var studioData = new StudioData();
            var service = new AppointmentService(studioData);

            // Add an artist on leave
            studioData.Leaves.Add(new Leave
            {
                ArtistId = 1,
                LeaveStart = new DateTime(2026, 08, 01, 09, 0, 0),
                LeaveEnd = new DateTime(2026, 08, 01, 17, 0, 0)
            });

            // Try to schedule an appointment during that leave
            var appointment = new Appointment
            {
                ArtistId = 1,
                AppointmentStart = new DateTime(2026, 08, 01, 10, 0, 0),
                AppointmentEnd = new DateTime(2026, 08, 01, 12, 0, 0)
            };

            // Act
            bool result = service.ScheduleAppointment(appointment);

            // Assert
            Assert.False(result, "Appointment should not be scheduled when artist is on leave.");
        }

        [Fact]
        public void ScheduleAppointment_ShouldReturnFalse_WhenOverlapping()
        {
            // Arrange
            var studioData = new StudioData();
            var service = new AppointmentService(studioData);

            // Add existing appointment
            studioData.Appointments.Add(new Appointment
            {
                ArtistId = 1,
                AppointmentStart = new DateTime(2026, 09, 01, 10, 0, 0),
                AppointmentEnd = new DateTime(2026, 09, 01, 12, 0, 0),
                Status = AppointmentStatus.Pending
            });

            // Try to book overlapping time
            var overlapAppointment = new Appointment
            {
                ArtistId = 1,
                AppointmentStart = new DateTime(2026, 09, 01, 11, 0, 0), // Starts during the first one
                AppointmentEnd = new DateTime(2026, 09, 01, 13, 0, 0)
            };

            // Act
            bool result = service.ScheduleAppointment(overlapAppointment);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ConfirmAppointment_ShouldReturnFalse_WhenNoDepositExists()
        {
            // Arrange
            var studioData = new StudioData();
            var service = new AppointmentService(studioData);

            // Add an appointment but NO payment
            studioData.Appointments.Add(new Appointment { AppointmentId = 99 });

            // Act
            bool result = service.ConfirmAppointment(99);

            // Assert
            Assert.False(result, "Appointment should not be confirmed without a deposit.");
        }

        [Fact]
        public void ScheduleAppointment_ShouldReturnFalse_WhenFollowUpToCancelledAppointment()
        {
            // Arrange
            var studioData = new StudioData();
            var service = new AppointmentService(studioData);

            // Add the "original" appointment as Cancelled
            studioData.Appointments.Add(new Appointment
            {
                AppointmentId = 10,
                Status = AppointmentStatus.Cancelled,
                ClientId = 1
            });

            // Try to schedule a follow-up to that cancelled ID
            var followUp = new Appointment
            {
                FollowupToAppointmentId = 10,
                ClientId = 1
            };

            // Act
            bool result = service.ScheduleAppointment(followUp);

            // Assert
            Assert.False(result, "Should not be able to follow up a cancelled appointment.");
        }
    }
}
