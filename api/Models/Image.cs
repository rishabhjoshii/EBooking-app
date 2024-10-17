using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace api.Models
{
    public class Image
    {
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string? FileName { get; set; }

        public string? FileDescription { get; set; }

        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }

        // Foreign key to establish relationship with Event
        public int EventId { get; set; }

        // Navigation property for Event
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
    }
}