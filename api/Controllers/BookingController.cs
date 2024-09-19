using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Booking;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IEventRepository _eventRepo;
        public BookingController(IBookingRepository bookingRepo, IEventRepository eventRepo)
        {
            _bookingRepo = bookingRepo;
            _eventRepo = eventRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookings = await _bookingRepo.GetAllAsync();
            var bookingDtos = bookings.Select(s => s.ToBookingDto()).ToList();

            return Ok(bookingDtos);
        }

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

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto bookingDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingModel = bookingDto.ToBookingFromCreateBookingDto();
            var eventId = bookingModel.EventId;
            
            var bookingStatus = await _bookingRepo.CreateAsync(bookingModel, eventId);
            if(bookingStatus==null){
                return Ok("either event does not exits or lesser ticket are avaiable");
            }

            return CreatedAtAction(nameof(GetById), new {id = bookingModel.Id}, bookingModel.ToBookingDto());
        }
    }
}