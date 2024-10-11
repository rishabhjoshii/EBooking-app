using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class UserInfoDto
    {
        public string message { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }
    }
}