using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wuzzfny.Domain.Common;

namespace Wuzzfny.Domain.Models.Users
{
    public class User : BaseEntity<long>
    {
        public UserRoles Role { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string Name { get; set; }
        public int? Experience_Years { get; set; }
        public string? Device_Token { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreationTime { get; set; }

    }

    public enum UserRoles
    {
        JobSeeker = 1,
        Admin = 2,
    }
}
