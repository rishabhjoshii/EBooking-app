using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class TicketType
    {
        
        public int Id { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public string TicketTypeName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; } = 0;

        [Required]
        public int TotalTickets { get; set; } = 0;

        public int BookedTickets { get; set; } = 0;

        //navigation property
        [ForeignKey("EventId")]
        public Event Event { get; set; }

    }
}