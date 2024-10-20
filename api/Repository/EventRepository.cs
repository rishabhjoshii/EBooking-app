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
using Microsoft.VisualBasic;

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
            Console.WriteLine("oh my gaushhhhh loooknhereeeee: ", eventModel.ToString());
            
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();

            return eventModel;
        }

        public async Task<Event?> DeleteAsync(int id, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try{
                var existingEvent = _context.Events.FirstOrDefault(x => x.Id == id);
                if(existingEvent == null || existingEvent.ApplicationUserId != userId){
                    return null;
                }

                // Removing all related bookings first
                _context.Bookings.RemoveRange(existingEvent.Bookings);

                //removing the event now
                _context.Events.Remove(existingEvent);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();


                return existingEvent;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task<List<Event>> GetAllAsync()
        {
            var events = await _context.Events
                .Include(e => e.Images)
                .Include(e => e.TicketTypes)  // Include TicketTypes as well
                .Include(e => e.ApplicationUser)
                .ToListAsync();

            return events;
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            try{
                var eventModel = await _context.Events
                    .Include(e => e.Images)
                    .Include(e => e.TicketTypes)
                    .Include(e => e.ApplicationUser)
                    .FirstOrDefaultAsync(x => x.Id == id);
                    
                return eventModel;
            }
            catch(Exception ex){
                return null;
            }
            
        }

        // public async Task<Event?> UpdateAsync(int id, Event eventModel)
        // {
        //     try{
        //         var existingEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
        //         if(existingEvent == null){
        //             return null;
        //         }
        //         existingEvent.EventName = eventModel.EventName;
        //         existingEvent.Date = eventModel.Date;
        //         existingEvent.Timing = eventModel.Timing;
        //         existingEvent.Venue = eventModel.Venue;
        //         existingEvent.Description = eventModel.Description;
        //         existingEvent.CategoryId = eventModel.CategoryId;
        //         existingEvent.TotalTickets = eventModel.TotalTickets;
        //         existingEvent.TicketPrice = eventModel.TicketPrice;

        //         await _context.SaveChangesAsync();

        //         return existingEvent;
        //     }
        //     catch(Exception e){
        //         Console.WriteLine(e.ToString());
        //         return null;
        //     }
            
        // }
    }
}