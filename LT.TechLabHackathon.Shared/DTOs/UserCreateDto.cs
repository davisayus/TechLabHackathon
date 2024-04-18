using LT.TechLabHackathon.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserCreateDto
    {
        public UserCreateDto(string name, string eMail, bool authDoublefactor, string picture, string phoneNumber, string password)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            if (string.IsNullOrEmpty(eMail))
                throw new ArgumentException($"'{nameof(eMail)}' cannot be null or empty.", nameof(eMail));

            if (string.IsNullOrEmpty(picture))
                throw new ArgumentException($"'{nameof(picture)}' cannot be null or empty.", nameof(picture));

            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException($"'{nameof(phoneNumber)}' cannot be null or empty.", nameof(phoneNumber));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));

            Name = name;
            Email = eMail;
            AuthDoublefactor = authDoublefactor;
            Picture = picture;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        public UserCreateDto()
        {
            Name ??= string.Empty;
            Email ??= string.Empty;
            Picture ??= string.Empty;
            PhoneNumber ??= string.Empty;
            Password ??= string.Empty;
        }

        [Required(ErrorMessage = "The Name attribute is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The E-Mail attribute is mandatory")]
        public string Email { get; set; }
        public bool AuthDoublefactor { get; set; }

        [Required(ErrorMessage = "The Picture attribute is mandatory")]
        public string Picture { get; set; }

        [Required(ErrorMessage = "The PhoneNumber attribute is mandatory")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The Password attribute is mandatory")]
        [StrongPassword]
        public string Password { get; set; }
    }
}
