using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class NewUserDto
    {
        public string Message { get; set; } =  "Default msge didn't override";
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PreferredCurrency { get; set; }
        public string PreferredLanguage { get; set; }

        public string? Token { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}