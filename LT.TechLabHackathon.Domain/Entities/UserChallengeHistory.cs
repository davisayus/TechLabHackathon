using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class UserChallengeHistory
    {
        public UserChallengeHistory()
        {
            CodeChallenge ??= string.Empty;    
        }

        [Key]
        public int UserChallengeHistoryId { get; set; }
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string CodeChallenge { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool Unlocked { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Challenge Challenge { get; set; } = null!;
    }
}
