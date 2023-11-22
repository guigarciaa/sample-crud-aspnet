using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleCrud.Domain.Entities
{
    public class Person
    {
        private List<string> _errors = new List<string>();

        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

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
        ]

        public DateOnly? Birthday { get; set; }

        public List<string> Stack { get; set; }

        public Person()
        {
            Stack = new List<string>();
        }

        public Person(string nickname, string name, string email, DateOnly birthday, List<string> stack)
        {
            Nickname = nickname;
            Name = name;
            Email = email;
            Birthday = birthday;
            Stack = stack;

            if (!IsValid())
            {
                throw new AggregateException(_errors.Select(x => new Exception(x)));
            }
        }


        public bool IsValid()
        {
            // Nickname Validations
            if (string.IsNullOrEmpty(Nickname))
            {
                _errors.Add("Nickname is required");
            }
            if (Nickname?.Length < 3 || Nickname?.Length > 32)
            {
                _errors.Add("Nickname must be between 3 and 32 characters");
            }

            // Name Validations
            if (string.IsNullOrEmpty(Name))
            {
                _errors.Add("Name is required");
            }
            if (Name?.Length < 3 || Name?.Length > 100)
            {
                _errors.Add("Name must be between 3 and 100 characters");
            }

            // Email Validations
            if (string.IsNullOrEmpty(Email))
            {
                _errors.Add("Email is required");
            }
            if (Email != null && !new EmailAddressAttribute().IsValid(Email))
            {
                _errors.Add("Invalid email address");
            }

            // Birthday Validations
            if (Birthday == null)
            {
                _errors.Add("Birthday is required");
            }

            // Stack Validations
            if (Stack != null && Stack.Count > 0)
            {
                var verifyLengthEnchStack = Stack.Exists(x => x.Length > 32);
                if (verifyLengthEnchStack)
                {
                    _errors.Add("Stack must be max 32 characters");
                }
            }

            return _errors.Count == 0;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nickname: {Nickname}, Name: {Name}, Email: {Email}, Birthday: {Birthday}, Stack: {Stack}";
        }

        public string ShowErrors()
        {
            return _errors?.ToString() ?? "";
        }
    }
}