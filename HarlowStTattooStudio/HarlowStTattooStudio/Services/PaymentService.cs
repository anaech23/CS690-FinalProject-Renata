using System;
using System.Collections.Generic;
using System.Text;
using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using System.Linq;

namespace HarlowStTattooStudio.Services
{
    public class PaymentService
    {
        private readonly StudioData _studioData;

        public PaymentService(StudioData studioData)
        {
            _studioData = studioData;
        }

        public bool RecordDeposit(int appointmentId, decimal amount)
        {
            var appointment = _studioData.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (appointment == null)
            {
                return false; // Appointment not found
            }
            var payment = new Payment
            {
                PaymentID = _studioData.NextPaymentId++,
                AppointmentId = appointmentId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                IsConfirmed = true, // Assuming payment is confirmed for simplicity
                ChargeType = ChargeType.Deposit
            };
            _studioData.Payments.Add(payment);
            _studioData.Save();
       
            return true;
        }

        public bool HasDeposit(int appointmentId)
        { 
            return _studioData.Payments.Any(p => p.AppointmentId == appointmentId && p.ChargeType == ChargeType.Deposit && p.IsConfirmed);
        }

        public bool RecordFullPayment(int appointmentId, decimal amount)
        {
            var appointment = _studioData.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (appointment == null)
            {
                return false; // Appointment not found
            }
            var payment = new Payment
            {
                PaymentID = _studioData.NextPaymentId++,
                AppointmentId = appointmentId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                IsConfirmed = true, // Assuming payment is confirmed for simplicity
                ChargeType = ChargeType.FullPayment
            };
            _studioData.Payments.Add(payment);
            _studioData.Save();
            return true;
        }

        public IEnumerable<Payment> GetPaymentsForAppointment(int appointmentId)
        {
            return _studioData.Payments.Where(p => p.AppointmentId == appointmentId);
        }

        
    }
}
