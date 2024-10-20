using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Event;
using api.Dtos.Image;
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
        private readonly ITicketTypeRepository _ticketTypeRepo;
        private readonly IImageRepository _imageRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public EventController(IEventRepository eventRepo, IBookingRepository bookingRepo, UserManager<ApplicationUser> userManager, IImageRepository imageRepo, ITicketTypeRepository ticketTypeRepo)
        {
            _eventRepo = eventRepo;
            _bookingRepo = bookingRepo;
            _userManager = userManager;
            _imageRepo = imageRepo;
            _ticketTypeRepo = ticketTypeRepo;

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
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if(eventDto.TicketTypes == null || eventDto.TicketTypes.Count == 0)
            {
                return BadRequest("No Ticket Types provided.");
            }

            try
            {
                var eventModel = eventDto.ToEventFromCreateEventDto(user.Id);
                Console.WriteLine("hereeee: ", eventModel);

                var createdEvent = await _eventRepo.CreateAsync(eventModel);

                return CreatedAtAction(nameof(GetById), new {id = eventModel.Id}, eventModel.ToEventDto());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while creating the event. Check if categoryId is valid or not.");
            }            
        }

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