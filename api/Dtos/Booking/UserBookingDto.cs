using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Booking
{
    public class UserBookingDto : BookingDto
    {
        public string? EventName { get; set; } = string.Empty;
        public string? EventLocation { get; set; } = string.Empty;
        public DateTime? EventDate { get; set; }

        public TimeSpan? EventTime { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}