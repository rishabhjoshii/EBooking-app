using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Booking
{
    public class CreateBookingDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public int EventId { get; set; } 
        public int NoOfTickets { get; set; }
        public int PricePaid { get; set; }
    }
}