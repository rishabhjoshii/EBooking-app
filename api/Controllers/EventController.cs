using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Event;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/Events")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public EventController(IEventRepository eventRepo, IBookingRepository bookingRepo, UserManager<ApplicationUser> userManager)
        {
            _eventRepo = eventRepo;
            _bookingRepo = bookingRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var events = await _eventRepo.GetAllAsync();
            var eventDtos = events.Select(s => s.ToEventDto()).ToList();

            return Ok(eventDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Console.WriteLine("control is reaching here");
            Console.WriteLine("model state is hereeeee : " ,ModelState);
            var eventModel = await _eventRepo.GetByIdAsync(id);

            if(eventModel==null){
                return NotFound("Event id is invalid or does not exist"); 
            }

            return Ok(eventModel.ToEventDto());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var eventModel = eventDto.ToEventFromCreateEventDto(user.Id);
            
            await _eventRepo.CreateAsync(eventModel);

            return CreatedAtAction(nameof(GetById), new {id = eventModel.Id}, eventModel.ToEventDto());
        }

        //to update event 
        // [Authorize]
        // [HttpPut]
        // [Route("{id:int}")]
        // public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] UpdateEventDto eventDto)
        // {
        //     if(!ModelState.IsValid){
        //         return BadRequest(ModelState);
        //     }
            
        //     var eventModel = eventDto.ToEventFromUpdateEventDto();
        //     var updatedEvent = await _eventRepo.UpdateAsync(id, eventModel);
        //     if(updatedEvent == null){
        //         return NotFound("event not found");
        //     }

        //     return Ok(updatedEvent.ToEventDto());
        // }

        [Authorize]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var eventModel = await _eventRepo.DeleteAsync(id,user.Id);
            if(eventModel == null){
                return NotFound("event not found or user is not the creator of the event");
            }

            return Ok(new { message = "Event deleted successfully", deletedEvent = eventModel });

        }
    }
}