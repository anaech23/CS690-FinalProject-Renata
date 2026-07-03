using System;
using System.Collections.Generic;
using System.Text;

namespace HarlowStTattooStudio.DomainModel
{
    public class Leave
    {
        public int LeaveId { get; set; }
        public DateTime LeaveStart { get; set; }
        public DateTime LeaveEnd { get; set; }
        public int ArtistId { get; set; }
    }
}
