using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wuzzfny.Domain.Models.Users;

namespace Wuzzfny.Application.Users.Dtos
{
    public class UserDto
    {
        public UserRoles Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int? Experience_Years { get; set; }
        public string PhoneNumber { get; set; }

    }
}
