using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.TicketType;
using api.Models;

namespace api.Dtos.Event
{
    public class CreateEventDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Event Name can't be more than 100 characters")]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Timing { get; set; }

        [Required]
        public string Venue { get; set; } = "TBD";

        [Required]
        [MaxLength(2000 , ErrorMessage = "event Description cannot be more than 2000 characters")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; } = 1;

        [Required]
        public List<CreateTicketTypeDto> TicketTypes { get; set; } = new List<CreateTicketTypeDto>();
    }
}