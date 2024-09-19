using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task<Booking?> CreateAsync(Booking bookingModel, int eventId);
    }
}