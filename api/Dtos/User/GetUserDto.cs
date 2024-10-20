using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;

namespace api.Dtos.User
{
    public class GetUserDto
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }
        public string PreferredCurrency { get; set; }
        public string PreferredLanguage { get; set; }

        public string? ProfileImageUrl { get; set; }

    }
}