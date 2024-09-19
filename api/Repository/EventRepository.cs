using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Event;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDBContext _context;
        public EventRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateAsync(Event eventModel)
        {
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();

            return eventModel;
        }

        public async Task<Event?> DeleteAsync(int id)
        {
            var existingEvent = _context.Events.FirstOrDefault(x => x.Id == id);
            if(existingEvent == null){
                return null;
            }

            _context.Events.Remove(existingEvent);
            await _context.SaveChangesAsync();

            return existingEvent;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            var events = await _context.Events.ToListAsync();
            return events;
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            try{
                var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
                return eventModel;
            }
            catch(Exception ex){
                return null;
            }
            
        }

        public async Task<Event?> UpdateAsync(int id, Event eventModel)
        {
            try{
                var existingEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
                if(existingEvent == null){
                    return null;
                }
                existingEvent.EventName = eventModel.EventName;
                existingEvent.Date = eventModel.Date;
                existingEvent.Timing = eventModel.Timing;
                existingEvent.Venue = eventModel.Venue;
                existingEvent.Description = eventModel.Description;
                existingEvent.CategoryId = eventModel.CategoryId;
                existingEvent.TotalTickets = eventModel.TotalTickets;
                existingEvent.TicketPrice = eventModel.TicketPrice;

                await _context.SaveChangesAsync();

                return existingEvent;
            }
            catch(Exception e){
                Console.WriteLine(e.ToString());
                return null;
            }
            
        }
    }
}