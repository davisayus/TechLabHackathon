using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class Records
    {
        public record RequestCompile(ChallengeDto Challenge, int ProgrammingLanguegeId, string Code);

        public record LoginResponseDto(string Token, UserDto User);
        public record LoginRequestDto(string UserName, string Password);
        public record LoginChangePasswordDto(string UserName, string CurrentPassword, string NewPassword);
    }
}
