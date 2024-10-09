using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Booking;
using api.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace api.Mappers
{
    public static class BookingMappers
    {
        public static BookingDto ToBookingDto(this Booking bookingModel)
        {
            return new BookingDto{
                Id = bookingModel.Id,
                Name = bookingModel.Username,
                Email = bookingModel.Email,
                PhoneNumber = bookingModel.PhoneNumber,
                EventId = bookingModel.EventId,
                NoOfTickets = bookingModel.NoOfTickets,
                PricePaid = bookingModel.PricePaid
            };
        }

        public static Booking ToBookingFromCreateBookingDto(this CreateBookingDto dtoModel, string userId)
        {
            return new Booking{
                Username = dtoModel.Username,
                Email = dtoModel.Email,
                PhoneNumber = dtoModel.PhoneNumber,
                EventId = dtoModel.EventId,
                NoOfTickets = dtoModel.NoOfTickets,
                PricePaid = dtoModel.PricePaid,
                ApplicationUserId = userId,
            };
        }

        public static UserBookingDto CreateUserBookingDTOFromBooking(this Booking bookings)
        {
            return new UserBookingDto
            {
                Id = bookings.Id,
                Name = bookings.Username,
                Email = bookings.Email,
                PhoneNumber = bookings.PhoneNumber,
                EventId = bookings.EventId,
                NoOfTickets = bookings.NoOfTickets,
                PricePaid = bookings.PricePaid,
                EventName = bookings.Event.EventName,
                EventLocation = bookings.Event.Venue,
                Description = bookings.Event.Description,
                EventDate = bookings.Event.Date,
                EventTime = bookings.Event.Timing
            };
        }
    }
}