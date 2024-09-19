using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Booking
    {
        //primary key
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }

        public int EventId { get; set; } //foreign key
        public DateTime BookedAt { get; set; }
        public int NoOfTickets { get; set; }
        public int PricePaid { get; set; }

        //navigation properties
        public Event Event { get; set; }

    }
}