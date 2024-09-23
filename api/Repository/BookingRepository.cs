using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDBContext _context;
        public BookingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Booking?> CreateAsync(Booking bookingModel, int eventId)
        {
            var eventModel = _context.Events.FirstOrDefault(e => e.Id == eventId);
            if(eventModel == null){
                return null;
            }
            var availableTickets = eventModel.TotalTickets - eventModel.BookedTickets;
            var ticketsRequired = bookingModel.NoOfTickets;

            if(availableTickets < ticketsRequired){
                return null;
            }
            await _context.Bookings.AddAsync(bookingModel);
            eventModel.BookedTickets += ticketsRequired;
            await _context.SaveChangesAsync();

            return bookingModel;
        }

        public async Task<List<Booking>?> DeleteAllAsync(int eventId)
        {
            var bookings = await _context.Bookings.Where(x => x.EventId == eventId).ToListAsync();
            if(bookings == null){
                return null;
            }

            _context.Bookings.RemoveRange(bookings);
            await _context.SaveChangesAsync();

            return bookings;

        }

        public async Task<List<Booking>> GetAllAsync()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return bookings;
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            try{
                var bookingModel = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
                return bookingModel;
            }
            catch(Exception ex){
                return null;
            }
        }
    }
}