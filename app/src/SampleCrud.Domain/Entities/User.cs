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

        [
            Required(ErrorMessage = "Email is required"),
            EmailAddress(ErrorMessage = "Invalid email address")
        ]
        public required string? Email { get; set; }

        public required string Password { get; set; }

        public required List<string> Roles { get; set; }

        public Person PersonId { get; set; }
    }
}