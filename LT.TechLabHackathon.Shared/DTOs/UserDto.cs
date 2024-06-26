﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserDto
    {
        public UserDto(int userId, string name, string eMail, bool authDoublefactor, string picture, string phoneNumber, int statusId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(eMail))
            {
                throw new ArgumentException($"'{nameof(eMail)}' cannot be null or empty.", nameof(eMail));
            }

            if (string.IsNullOrEmpty(picture))
            {
                throw new ArgumentException($"'{nameof(picture)}' cannot be null or empty.", nameof(picture));
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException($"'{nameof(phoneNumber)}' cannot be null or empty.", nameof(phoneNumber));
            }

            UserId = userId;
            Name = name;
            Email = eMail;
            AuthDoublefactor = authDoublefactor;
            Picture = picture;
            PhoneNumber = phoneNumber;
            StatusId = statusId;
        }

        public UserDto()
        {
            Name ??= string.Empty;
            Email ??= string.Empty;
            Picture ??= string.Empty;
            PhoneNumber ??= string.Empty;
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool AuthDoublefactor { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
        public int StatusId { get; set; }
    }
}
