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
            // 1. Arrange: Create the fake data
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

            // 2. Act
            bool result = service.ScheduleAppointment(appointment);

            // 3. Assert
            Assert.False(result, "Appointment should not be scheduled when artist is on leave.");
        }
    }
}
