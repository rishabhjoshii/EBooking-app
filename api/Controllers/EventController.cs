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
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IImageRepository _imageRepo;
        public EventController(IEventRepository eventRepo, IBookingRepository bookingRepo, UserManager<ApplicationUser> userManager, IImageRepository imageRepo)
        {
            _eventRepo = eventRepo;
            _bookingRepo = bookingRepo;
            _userManager = userManager;
            _imageRepo = imageRepo;
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
        public async Task<IActionResult> CreateEvent([FromForm] CreateEventDto eventDto, [FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

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
            
            var createdEvent = await _eventRepo.CreateAsync(eventModel);

            foreach (var file in imageUploadRequestDto.Files)
            {
                // Optionally, generate a unique file name here if needed
                var uniqueFileName = $"{Path.GetFileNameWithoutExtension(imageUploadRequestDto.FileName)}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                var imageDomainModel = new Image
                {
                    File = file,
                    FileExtension = Path.GetExtension(file.FileName),
                    FileSizeInBytes = file.Length,
                    FileName = uniqueFileName,  // Use the same file name or generate unique ones
                    FileDescription = imageUploadRequestDto.FileDescription,
                    EventId = createdEvent.Id,
                };

                await _imageRepo.Upload(imageDomainModel);
            }


            return CreatedAtAction(nameof(GetById), new {id = eventModel.Id}, eventModel.ToEventDto());
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


        // function to validate image upload
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpeg", ".jpg", ".png"};

            foreach (var file in request.Files)
            {
                // Check if the file is not null
                if (file == null)
                {
                    ModelState.AddModelError("file", "One or more files are missing.");
                    continue;
                }

                // Check if the image extension is valid
                if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    ModelState.AddModelError("file", $"Unsupported file extension: {file.FileName}");
                }

                // Validate file size (max 5MB)
                if (file.Length > 5242880)
                {
                    ModelState.AddModelError("file", $"File size exceeds 5MB: {file.FileName}");
                }
            }
        }
    }
}