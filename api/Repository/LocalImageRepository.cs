using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDBContext dbContext;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDBContext dbContext) 
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<UserProfileImage?> GetUserProfileImageByUserId(string userId)
        {
            return await dbContext.UserProfileImages.FirstOrDefaultAsync(img => img.ApplicationUserId == userId);
        }

        public async Task<Image> Upload(Image image)
        {
            // Combine the file name and extension correctly
            var fileNameWithExtension = $"{image.FileName}{image.FileExtension}";

            // Ensure the Images directory exists
            var imagesDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            // Construct the full local file path
            var localFilePath = Path.Combine(imagesDirectory, fileNameWithExtension);

            // Upload image to the local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // Construct the URL for the image
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{fileNameWithExtension}";

            // Save the image's URL in the database
            image.FilePath = urlFilePath;
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }

        public async Task<UserProfileImage> UploadUserProfileImage(UserProfileImage userProfileImage)
        {
            // Combine the file name and extension correctly
            var fileNameWithExtension = $"{userProfileImage.FileName}{userProfileImage.FileExtension}";

            // Ensure the ProfileImages directory exists
            var profileImagesDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "ProfileImages");
            if (!Directory.Exists(profileImagesDirectory))
            {
                Directory.CreateDirectory(profileImagesDirectory);
            }

            // Construct the full local file path
            var localFilePath = Path.Combine(profileImagesDirectory, fileNameWithExtension);

            // Upload profile image to the local path
            using (var stream = new FileStream(localFilePath, FileMode.Create))
            {
                await userProfileImage.File.CopyToAsync(stream);
            }

            // Construct the URL for the profile image
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/ProfileImages/{fileNameWithExtension}";

            // Save the image's URL and other details in the database
            userProfileImage.FilePath = urlFilePath;
            await dbContext.UserProfileImages.AddAsync(userProfileImage);
            await dbContext.SaveChangesAsync();

            return userProfileImage;
        }

        public async Task<UserProfileImage> UpdateUserProfileImage(UserProfileImage userProfileImage)
        {
            dbContext.UserProfileImages.Update(userProfileImage);
            await dbContext.SaveChangesAsync();
            return userProfileImage;
        }


    }
}