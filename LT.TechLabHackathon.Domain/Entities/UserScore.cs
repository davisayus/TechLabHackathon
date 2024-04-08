using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class UserScore
    {
        [Key]
        public int UserScoreId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int UnlockedChallenges { get; set; }

        public virtual User User { get; set; } = null!;

    }
}
