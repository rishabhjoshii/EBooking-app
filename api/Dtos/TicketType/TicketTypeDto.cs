using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.TicketType
{
    public class TicketTypeDto
    {
        [Required]
        public string TicketTypeName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; } = 0;

        [Required]
        public int TotalTickets { get; set; } = 0;

        public int BookedTickets { get; set; }

    }
}