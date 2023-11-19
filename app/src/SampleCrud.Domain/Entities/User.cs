using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SampleCrud.Domain.Entities
{
    public class User
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Email { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

        public Person? PersonId { get; set; }

        public User(string email, string password, string roles)
        {
            Email = email;
            Password = password;
            Roles = roles;
        }
    }
}