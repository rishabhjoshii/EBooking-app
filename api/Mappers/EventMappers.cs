using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Models;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace api.Mappers
{
    public static class EventMappers
    {
        public static Event ToEventFromCreateEventDto(this CreateEventDto eventDto, string userId)
        {
            return new Event{
                EventName = eventDto.EventName,
                Date = eventDto.Date,
                Timing = eventDto.Timing,
                Venue = eventDto.Venue,
                Description = eventDto.Description,
                TicketPrice = eventDto.TicketPrice,
                TotalTickets = eventDto.TotalTickets,
                CategoryId = eventDto.CategoryId,
                ApplicationUserId = userId,
            };
        }

        public static Event ToEventFromUpdateEventDto(this UpdateEventDto eventDto)
        {
            return new Event{
                EventName = eventDto.EventName,
                Date = eventDto.Date,
                Timing = eventDto.Timing,
                Venue = eventDto.Venue,
                Description = eventDto.Description,
                TicketPrice = eventDto.TicketPrice,
                TotalTickets = eventDto.TotalTickets,
                CategoryId = eventDto.CategoryId,
            };
        }

        public static EventDto ToEventDto(this Event eventModel)
        {
            return new EventDto{
                Id = eventModel.Id,
                EventName = eventModel.EventName,
                Date = eventModel.Date,
                Timing = eventModel.Timing,
                Venue = eventModel.Venue,
                Description = eventModel.Description,
                TicketPrice = eventModel.TicketPrice,
                TotalTickets = eventModel.TotalTickets,
                BookedTickets = eventModel.BookedTickets,
                CategoryId = eventModel.CategoryId,
            };
        }

        
    }
}