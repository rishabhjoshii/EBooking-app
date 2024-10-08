using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public int EventId { get; set; } 
        public int NoOfTickets { get; set; }
        public int PricePaid { get; set; }
    }
}