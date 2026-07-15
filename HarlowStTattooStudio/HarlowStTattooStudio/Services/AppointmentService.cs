using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HarlowStTattooStudio.Services
{
    public class AppointmentService
    {
        private readonly StudioData _studioData;

        public AppointmentService(StudioData studioData)
        {
            _studioData = studioData;
        }


        // Create a new appointment request
        public bool ScheduleAppointment(Appointment newAppointment)
        {
            // Check if this is a follow-up appointment
            if (newAppointment.FollowupToAppointmentId.HasValue)
            {
                var originalAppointment = _studioData.Appointments.FirstOrDefault(a => a.AppointmentId == newAppointment.FollowupToAppointmentId.Value);

                // Original appointment must exist
                if (originalAppointment == null)
                {
                    return false;
                }

                // Cannot follow up a cancelled appointment
                if (originalAppointment.Status == AppointmentStatus.Cancelled)
                {
                    return false;
                }

                // Follow-up must belong to the same client
                if (originalAppointment.ClientId != newAppointment.ClientId)
                {
                    return false;
                }
            }


            // Check if artist is on leave
            bool isArtistOnLeave = _studioData.Leaves.Any(l => l.ArtistId == newAppointment.ArtistId && newAppointment.AppointmentStart >= l.LeaveStart && newAppointment.AppointmentEnd <= l.LeaveEnd);

            if (isArtistOnLeave)
            {
                return false;
            }


            // Check for overlapping appointments
            bool hasOverlap = _studioData.Appointments.Any(a => a.ArtistId == newAppointment.ArtistId && a.Status != AppointmentStatus.Cancelled && newAppointment.AppointmentStart < a.AppointmentEnd && newAppointment.AppointmentEnd > a.AppointmentStart);

            if (hasOverlap)
            {
                return false;
            }


            // Create appointment as pending until deposit is received
            newAppointment.AppointmentId = _studioData.NextAppointmentId++;
            newAppointment.Status = AppointmentStatus.Pending;

            _studioData.Appointments.Add(newAppointment);
            _studioData.Save();
            return true;
        }


        // Confirm appointment after deposit payment
        public bool ConfirmAppointment(int appointmentId)
        {
            var appointment = _studioData.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                return false;
            }

            // Check for confirmed deposit
            bool hasDeposit = _studioData.Payments.Any(p => p.AppointmentId == appointmentId && p.ChargeType == ChargeType.Deposit && p.IsConfirmed);

            if (!hasDeposit)
            {
                return false;
            }

            appointment.Status = AppointmentStatus.Scheduled;
            _studioData.Save();
            return true;
        }


        // Get all appointments
        public IEnumerable<Appointment> GetAll()
        {
            return _studioData.Appointments;
        }


        // Get past appointments for follow-up selection
        public IEnumerable<Appointment> GetPastAppointments(int clientId)
        {
            return _studioData.Appointments.Where(a => a.ClientId == clientId && a.Status != AppointmentStatus.Cancelled && a.AppointmentEnd < DateTime.Now) .OrderByDescending(a => a.AppointmentStart);
        }


        // Get the appointment that a follow-up is linked to
        public Appointment GetOriginalAppointment(int appointmentId)
        {
            var appointment = _studioData.Appointments
                .FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment == null ||
                !appointment.FollowupToAppointmentId.HasValue)
            {
                return null;
            }

            return _studioData.Appointments.FirstOrDefault(a =>a.AppointmentId == appointment.FollowupToAppointmentId.Value);
        }


        // Cancel appointment
        public bool CancelAppointment(int appointmentId)
        {
            var appointment = _studioData.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment == null || appointment.Status == AppointmentStatus.Cancelled)
            {
                return false;
            }

            appointment.Status = AppointmentStatus.Cancelled;
            _studioData.Save();
            return true;
        }
    }
}