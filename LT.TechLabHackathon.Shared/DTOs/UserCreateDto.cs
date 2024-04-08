using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserCreateDto
    {
        public UserCreateDto(string name, string eMail, bool authDoublefactor, string picture, string phoneNumber)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            if (string.IsNullOrEmpty(eMail))
                throw new ArgumentException($"'{nameof(eMail)}' cannot be null or empty.", nameof(eMail));

            if (string.IsNullOrEmpty(picture))
                throw new ArgumentException($"'{nameof(picture)}' cannot be null or empty.", nameof(picture));

            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException($"'{nameof(phoneNumber)}' cannot be null or empty.", nameof(phoneNumber));

            Name = name;
            Email = eMail;
            AuthDoublefactor = authDoublefactor;
            Picture = picture;
            PhoneNumber = phoneNumber;
        }

        public UserCreateDto()
        {
            Name ??= string.Empty;
            Email ??= string.Empty;
            Picture ??= string.Empty;
            PhoneNumber ??= string.Empty;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool AuthDoublefactor { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
    }
}
