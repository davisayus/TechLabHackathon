using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Validations
{
    public class StrongPasswordAttribute:ValidationAttribute 
    {
        public override bool IsValid(object value)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
                return false;

            // Password strength: Ensure that the password contains at least one lowercase letter,
            // one uppercase letter, one numeric digit and is at least 6 characters long.
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$");
            return regex.IsMatch(password);
        }

        public override string FormatErrorMessage(string name)
        {
            return "The password must have at least 6 characters, at least one uppercase letter, one lowercase letter and one number.";
        }
    }
}
