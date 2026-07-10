using System;
using System.Collections.Generic;
using System.Text;
using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using System.Linq;

namespace HarlowStTattooStudio.Services
{
    public class ReportService
    {
        private readonly StudioData _studioData;

        public ReportService(StudioData studioData)
        {
            _studioData = studioData;
        }

        // Total revenue for all confirmed appointments

        public decimal GetConfirmedAppointmentsRevenue()
        {
            return _studioData.Payments
                .Where(p => p.IsConfirmed)
                .Sum(p => p.Amount);
        }

        // Total deposits collected for all confirmed appointments
        public decimal GetTotalDeposits()
        {
            return _studioData.Payments
                .Where(p => p.IsConfirmed && p.ChargeType == ChargeType.Deposit)
                .Sum(p => p.Amount);
        }

        // Revenue by date range
        public decimal GetRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            return _studioData.Payments
                .Where(p => p.IsConfirmed && p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                .Sum(p => p.Amount);
        }
    }
}
