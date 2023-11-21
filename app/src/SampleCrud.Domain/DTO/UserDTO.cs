using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCrud.Domain.DTO
{
    public class UserDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Roles { get; set; }
    }
}