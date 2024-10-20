using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.TicketType;
using api.Dtos.User;
using api.Models;

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
        
        public int CategoryId { get; set; }

        public List<string> ImagePaths { get; set; } = new List<string>();

        public List<TicketTypeDto> TicketTypes { get; set; } = new List<TicketTypeDto>();

        public GetUserDto OrganiserDetails { get; set; } = new GetUserDto();
    }
}