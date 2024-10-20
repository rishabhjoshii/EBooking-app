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
        [MaxLength(100 , ErrorMessage = "event name cannot be more than 100 characters")]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Timing { get; set; }
        public string Venue { get; set; } = "TBD";

        [MaxLength(2000 , ErrorMessage = "event Description cannot be more than 2000 characters")]
        public string Description { get; set; } = string.Empty;
        public decimal TicketPrice { get; set; } = 0;
        public int TotalTickets { get; set; } = 0;
        public int CategoryId { get; set; } = 1;
    }
}