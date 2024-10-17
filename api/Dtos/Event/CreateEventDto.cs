using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Event
{
    public class CreateEventDto
    {
        [Required]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Timing { get; set; }

        [Required]
        public string Venue { get; set; } = "TBD";

        public string Description { get; set; } = string.Empty;

        [Required]
        public int TicketPrice { get; set; } = 0;

        [Required]
        public int TotalTickets { get; set; } = 0;

        [Required]
        public int CategoryId { get; set; } = 1;
    }
}