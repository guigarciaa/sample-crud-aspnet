using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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
        public DateTime Birthday { get; set; }

        public List<string> Stack { get; set; }

        public Person()
        {
            Stack = new List<string>();
        }

        public Person(string nickname, string name, string email, DateTime birthday, List<string> stack)
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
            #region Nickname Validations

            if (string.IsNullOrEmpty(Nickname))
            {
                _errors.Add("Nickname is required");
            }
            if (Nickname?.Length < 3 || Nickname?.Length > 32)
            {
                _errors.Add("Nickname must be between 3 and 32 characters");
            }

            #endregion

            #region Name Validations

            if (string.IsNullOrEmpty(Name))
            {
                _errors.Add("Name is required");
            }
            if (Name?.Length < 3 || Name?.Length > 100)
            {
                _errors.Add("Name must be between 3 and 100 characters");
            }
            var regex = new Regex(@"(.*)String|string|test|teste|null|nill|undefined*\w+");
            var isString = regex.IsMatch(Name ?? "");
            if (isString)
            {
                _errors.Add("Name not must be a word of list [String, string, test, teste, null, nil, undefined]");
            }

            #endregion

            #region Email Validations

            if (string.IsNullOrEmpty(Email))
            {
                _errors.Add("Email is required");
            }
            if (Email != null && !new EmailAddressAttribute().IsValid(Email))
            {
                _errors.Add("Invalid email address");
            }

            #endregion

            #region Stack Validations

            if (Stack != null && Stack.Count > 0)
            {   
                var verifyLengthEnchStack = Stack.Exists(x => x.Length > 32);
                if (verifyLengthEnchStack)
                {
                    _errors.Add("Stack must be max 32 characters");
                }
            }

            #endregion

            return _errors.Count == 0;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nickname: {Nickname}, Name: {Name}, Email: {Email}, Birthday: {Birthday}, Stack: {Stack}";
        }

        public string ShowErrors()
        {
            return string.Join(Environment.NewLine, _errors);
        }
    }
}