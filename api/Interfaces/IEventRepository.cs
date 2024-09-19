using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Models;

namespace api.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();

        Task<Event?> GetByIdAsync(int id);

        Task<Event> CreateAsync(Event eventModel);

        Task<Event?> UpdateAsync(int id, Event eventModel);

        Task<Event?> DeleteAsync(int id);
    }
}