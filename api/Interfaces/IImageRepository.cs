using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);

        Task<UserProfileImage> UploadUserProfileImage(UserProfileImage userProfileImage);

        Task<UserProfileImage?> GetUserProfileImageByUserId(string userId);

        Task<UserProfileImage> UpdateUserProfileImage(UserProfileImage userProfileImage);
    }
}