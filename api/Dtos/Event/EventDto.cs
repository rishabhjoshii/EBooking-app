using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Event
{
    public class EventDto
    {
        public int Id { get; set; }
        public string EventName { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public TimeSpan Timing { get; set; }
        public string Venue { get; set; } = "TBD";
        public string Description { get; set; } = string.Empty;
        public int TicketPrice { get; set; }
        public int TotalTickets { get; set; }

        public int BookedTickets { get; set; }
        public int CategoryId { get; set; }
    }
}