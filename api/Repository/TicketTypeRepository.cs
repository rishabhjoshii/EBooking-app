using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly ApplicationDBContext _context;

        public TicketTypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<TicketType> CreateAsync(TicketType ticketTypeModel)
        {
            await _context.TicketTypes.AddAsync(ticketTypeModel);
            await _context.SaveChangesAsync();

            return ticketTypeModel;
        }
    }
}