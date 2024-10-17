using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Event
{
    public class UpdateEventDto
    {
        [Required]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Timing { get; set; }
        public string Venue { get; set; } = "TBD";
        public string Description { get; set; } = string.Empty;
        public decimal TicketPrice { get; set; } = 0;
        public int TotalTickets { get; set; } = 0;
        public int CategoryId { get; set; } = 1;
    }
}