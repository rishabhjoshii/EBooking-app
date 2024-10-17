using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Booking;
using api.Dtos.Image;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IEventRepository _eventRepo;

        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(IBookingRepository bookingRepo, IEventRepository eventRepo, UserManager<ApplicationUser> userManager)
        {
            _bookingRepo = bookingRepo;
            _eventRepo = eventRepo;
            _userManager = userManager;
        }

        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string UserName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(UserName);
            if (appUser == null)
            {
                return Unauthorized("User not found");
            }
            
            var bookings = await _bookingRepo.GetAllAsync(appUser.Id);
            var bookingDtos = bookings.Select(s => s.CreateUserBookingDTOFromBooking()).ToList();

            return Ok(bookingDtos);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var bookingModel = await _bookingRepo.GetByIdAsync(id);

            if(bookingModel==null){
                return NotFound("booking id is invalid or does not exist"); 
            }

            return Ok(bookingModel.ToBookingDto());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto bookingDto)
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
            
            var bookingModel = bookingDto.ToBookingFromCreateBookingDto(user.Id);
            var eventId = bookingModel.EventId;
            
            var bookingStatus = await _bookingRepo.CreateAsync(bookingModel, eventId);
            if(bookingStatus==null){
                return Ok("either event does not exits or lesser ticket are avaiable or pricePaid is not justified");
            }

            return CreatedAtAction(nameof(GetById), new {id = bookingModel.Id}, bookingModel.ToBookingDto());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] int id)
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized("User not found");
            }
            var result = await _bookingRepo.DeleteByIdAsync(id,user.Id);
            if(result == null){
                return BadRequest("Booking not found or you are not authorized to delete this booking");
            }
            return Ok("booking deleted successfully");
        }

        
    }
}