using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCrud.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }

        [Required]
        public string? Nickname { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }
        public DateOnly? Birthday { get; set; }
        public IEnumerable<string>? Stack { get; set; }
    }
}