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

        // public async Task<Booking?> CreateAsync(Booking bookingModel, int eventId)
        // {
        //     var eventModel = _context.Events.FirstOrDefault(e => e.Id == eventId);
        //     if(eventModel == null){
        //         return null;
        //     }
        //     var availableTickets = eventModel.TotalTickets - eventModel.BookedTickets;
        //     var ticketsRequired = bookingModel.NoOfTickets;
        //     var ticketPrice = eventModel.TicketPrice;
        //     var pricePaid = bookingModel.PricePaid;

        //     if((availableTickets < ticketsRequired) || (pricePaid != ticketPrice*ticketsRequired)){
        //         return null;
        //     }
        //     await _context.Bookings.AddAsync(bookingModel);
        //     eventModel.BookedTickets += ticketsRequired;
        //     await _context.SaveChangesAsync();

        //     return bookingModel;
        // }

        // public async Task<List<Booking>?> DeleteAllAsync(int eventId)
        // {
        //     var bookings = await _context.Bookings.Where(x => x.EventId == eventId).ToListAsync();
        //     if(bookings == null){
        //         return null;
        //     }

        //     _context.Bookings.RemoveRange(bookings);
        //     await _context.SaveChangesAsync();

        //     return bookings;

        // }

        // public async Task<Booking?> DeleteByIdAsync(int id, string userId)
        // {
        //     using var transaction = await _context.Database.BeginTransactionAsync();
        //     try{
        //         var bookingModel = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);

        //         if(bookingModel == null || bookingModel.ApplicationUserId != userId){
        //             return null; // Return null if booking not found or not owned by the user
        //         }

        //         // Get the number of tickets and event details
        //         var bookedTickets = bookingModel.NoOfTickets;
        //         var eventId = bookingModel.EventId;

        //         // Fetch the event related to the booking
        //         var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
        //         if(eventModel==null) return null;
        //         eventModel.BookedTickets = Math.Max(0, eventModel.BookedTickets - bookedTickets);

        //         _context.Bookings.Remove(bookingModel);
        //         await _context.SaveChangesAsync();

        //         await transaction.CommitAsync();

        //         return bookingModel;
        //     }
        //     catch(Exception ex){
        //         await transaction.RollbackAsync();
        //         Console.WriteLine(ex.Message);
        //         return null;
        //     }
            
        // }

        // public async Task<List<Booking>> GetAllAsync(string appUserId)
        // {
        //     var bookings = await _context.Bookings.Include(b => b.Event).Where(b => b.ApplicationUserId == appUserId).ToListAsync();
        //     return bookings;
        // }

        // public async Task<Booking?> GetByIdAsync(int id)
        // {
        //     try{
        //         var bookingModel = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        //         if(bookingModel==null) return null;
        //         return bookingModel;
        //     }
        //     catch(Exception ex){
        //         return null;
        //     }
        // }
    }
}