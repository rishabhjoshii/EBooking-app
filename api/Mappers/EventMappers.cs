using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Dtos.TicketType;
using api.Dtos.User;
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
                CategoryId = eventDto.CategoryId,
                ApplicationUserId = userId,
                TicketTypes = eventDto.TicketTypes.Select(tt => new TicketType
                {
                    TicketTypeName = tt.TicketTypeName,
                    TicketPrice = tt.TicketPrice,
                    TotalTickets = tt.TotalTickets,
                    BookedTickets = 0,
                }).ToList()
            };
        }

        // public static Event ToEventFromUpdateEventDto(this UpdateEventDto eventDto)
        // {
        //     return new Event{
        //         EventName = eventDto.EventName,
        //         Date = eventDto.Date,
        //         Timing = eventDto.Timing,
        //         Venue = eventDto.Venue,
        //         Description = eventDto.Description,
        //         TicketPrice = eventDto.TicketPrice,
        //         TotalTickets = eventDto.TotalTickets,
        //         CategoryId = eventDto.CategoryId,
        //     };
        // }

        public static EventDto ToEventDto(this Event eventModel)
        {
            return new EventDto{
                Id = eventModel.Id,
                EventName = eventModel.EventName,
                Date = eventModel.Date,
                Timing = eventModel.Timing,
                Venue = eventModel.Venue,
                Description = eventModel.Description,
                CategoryId = eventModel.CategoryId,
                ImagePaths = eventModel.Images.Select(e => e.FilePath).ToList(),
                TicketTypes = eventModel.TicketTypes.Select(tt => new TicketTypeDto
                {
                    TicketTypeName = tt.TicketTypeName,
                    TicketPrice = tt.TicketPrice,
                    TotalTickets = tt.TotalTickets,
                    BookedTickets = tt.BookedTickets,
                }).ToList(),
                OrganiserDetails = new GetUserDto{
                    UserName = eventModel.ApplicationUser.UserName,
                    Email = eventModel.ApplicationUser.Email,
                    PhoneNumber = eventModel.ApplicationUser.PhoneNumber,
                    FirstName = eventModel.ApplicationUser.FirstName,
                    LastName = eventModel.ApplicationUser.LastName,
                },
                
            };
        }

        
    }
}