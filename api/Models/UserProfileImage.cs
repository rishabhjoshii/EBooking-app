using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserProfileImage
    {
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string? FileName { get; set; }

        public string FileExtension { get; set; }
        
        public long FileSizeInBytes { get; set; }
        
        public string FilePath { get; set; }

        // Foreign key to ApplicationUser
        public string ApplicationUserId { get; set; }

        // Navigation property back to ApplicationUser
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}