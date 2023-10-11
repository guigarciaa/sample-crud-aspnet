using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SampleCrud.Domain.Entities
{
    public class Person
    {
        private Guid? _id;
        public Guid? Id
        {
            get => _id;
            set => _id = Guid.NewGuid();
        }

        [
            Required(ErrorMessage = "Nickname is required"),
            StringLength(32, MinimumLength = 3, ErrorMessage = "Nickname must be between 3 and 32 characters")
        ]
        public string? Nickname { get; set; }

        [
            Required(ErrorMessage = "Name is required"),
            StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")
        ]
        public string? Name { get; set; }

        [
            Required(ErrorMessage = "Email is required"),
            EmailAddress(ErrorMessage = "Invalid email address")
        ]
        public string? Email { get; set; }

        [
            Required(ErrorMessage = "Birthday is required"),
            DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)
        ]
        public DateOnly? Birthday { get; set; }

        public List<string>? Stack { get; set; }
    }
}