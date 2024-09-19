using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class EventCategory
    {

        public int Id { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        //navigation property
        public List<Event> Events { get; set; } = new List<Event>();
    }
}