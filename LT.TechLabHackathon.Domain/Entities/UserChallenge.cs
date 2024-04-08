using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class UserChallenge
    {
        [Key]
        public int UserChallengeId { get; set; }
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public bool Unlocked { get; set; }
        public DateTime? UnlockedDate { get; set; }
        public int UnlokedTime { get; set; }
        public int Attemps { get; set; }
        public bool Penalized { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Challenge Challenge { get; set; } = null!;
    }
}
