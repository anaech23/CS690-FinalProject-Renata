using System;
using System.Collections.Generic;
using System.Text;
using HarlowStTattooStudio.Data;
using HarlowStTattooStudio.DomainModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace HarlowStTattooStudio.Services
{
    public class LeaveService
    {
        private readonly StudioData _studioData;

        public LeaveService(StudioData studioData)
        {
            _studioData = studioData;
        }
        public bool ScheduleLeave(Leave newLeave)
        {
            // Check for valid leave dates
            if (newLeave.LeaveStart >= newLeave.LeaveEnd)
            {
                return false; // Invalid leave dates
            }

            // Check for overlapping leaves for the same artist
            bool hasOverlap = _studioData.Leaves.Any(l =>
                l.ArtistId == newLeave.ArtistId &&
                newLeave.LeaveStart < l.LeaveEnd &&
                newLeave.LeaveEnd > l.LeaveStart);
            if (hasOverlap)
            {
                return false; // Overlapping leave exists
            }

            newLeave.LeaveId = _studioData.NextLeaveId++; // Assign a new LeaveId
            _studioData.Leaves.Add(newLeave);
            return true; // Leave scheduled successfully
        }

        // View all leave for selected artist
        public List<Leave> GetArtistLeave(int artistId)
        {
            return _studioData.Leaves.Where(l => l.ArtistId == artistId).ToList();
        }
    }
}
