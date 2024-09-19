using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Booking;
using api.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace api.Mappers
{
    public static class BookingMappers
    {
        public static BookingDto ToBookingDto(this Booking bookingModel)
        {
            return new BookingDto{
                Id = bookingModel.Id,
                Username = bookingModel.Username,
                Email = bookingModel.Email,
                PhoneNumber = bookingModel.PhoneNumber,
                EventId = bookingModel.EventId,
                NoOfTickets = bookingModel.NoOfTickets,
                PricePaid = bookingModel.PricePaid
            };
        }

        public static Booking ToBookingFromCreateBookingDto(this CreateBookingDto dtoModel)
        {
            return new Booking{
                Username = dtoModel.Username,
                Email = dtoModel.Email,
                PhoneNumber = dtoModel.PhoneNumber,
                EventId = dtoModel.EventId,
                NoOfTickets = dtoModel.NoOfTickets,
                PricePaid = dtoModel.PricePaid
            };
        }
    }
}