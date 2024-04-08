using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserChallengeDto
    {
        public int UserChallengeId { get; set; }
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public bool Unlocked { get; set; }
        public DateTime? UnlockedDate { get; set; }
        public int UnlokedTime { get; set; }
        public int Attemps { get; set; }
        public bool Penalized { get; set; }

        public virtual UserDto User { get; set; } = null!;
        public virtual ChallengeDto Challenge { get; set; } = null!;
    }
}
