using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCrud.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }

        [Required]
        public string? Nickname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        public DateOnly? Birthday { get; set; }

        public IEnumerable<string>? Stack { get; set; }
    }
}