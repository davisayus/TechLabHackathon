using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserChallengeHistoryCreateDto
    {
        public UserChallengeHistoryCreateDto()
        {
            CodeChallenge ??= string.Empty;    
        }

        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string CodeChallenge { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool Unlocked { get; set; }
    }
}
