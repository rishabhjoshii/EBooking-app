using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Image;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/EventImage")]
    public class EventImageController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEventRepository _eventRepo;
        private readonly IImageRepository _imageRepo;
        public EventImageController(UserManager<ApplicationUser> userManager, IEventRepository eventRepo, IImageRepository imageRepo)
        {
            _userManager = userManager;
            _eventRepo = eventRepo;
            _imageRepo = imageRepo;
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> AddEventImage([FromRoute] int id, [FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            //check if the image request is valid or not , so that it can add invalidations to modelstate
            ValidateFileUpload(imageUploadRequestDto);

            //check if the model is valid or not 
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            //check if the user is authenticated or not
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            //check if the eventId is valid or not
            var eventModel = await _eventRepo.GetByIdAsync(id);
            if(eventModel == null) return BadRequest("Invalid event id");

            //check if the user is the creator of the event
            if(eventModel.ApplicationUserId != user.Id)
            {
                return Unauthorized("user is not the owner of the event. Only owner can access this functionality.");
            }
            
            //everything  is valid , perform acutual task
            foreach (var file in imageUploadRequestDto.Files)
            {
                //imageDto to actual image format mapping for database update
                var imageDomainModel = new Image
                {
                    File = file,
                    FileExtension = Path.GetExtension(file.FileName),
                    FileSizeInBytes = file.Length,
                    FileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}",  // Generating GUID filename
                    FileDescription = null,
                    EventId = id,
                };
                await _imageRepo.Upload(imageDomainModel);
            }

            return Ok("image(s) uploaded successfully");
        }

        // function to validate image upload
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            try
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("file", "some error occured regarding file upload");
            }
        }
        
    }
}