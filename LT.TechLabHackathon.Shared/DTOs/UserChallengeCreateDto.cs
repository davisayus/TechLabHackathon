using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserChallengeCreateDto
    {
        public UserChallengeCreateDto()
        {
            CodeChallenge ??= string.Empty;
        }

        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string CodeChallenge { get; set; }
        public bool Unlocked { get; set; }
        public DateTime? UnlockedDate { get; set; }
        public int UnlokedTime { get; set; }
        public int Attemps { get; set; }
        public bool Penalized { get; set; }
    }
}
