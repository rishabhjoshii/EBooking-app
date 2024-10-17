using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;

namespace api.Dtos.Image
{
    public class ImageUploadRequestDto
    {
        [Required]
         public List<IFormFile> Files { get; set; }  // Accept multiple files

         [Required]
        public string FileName { get; set; }  // Only one filename for all files
        public string? FileDescription { get; set; }  // Only one description for all files
    }
}