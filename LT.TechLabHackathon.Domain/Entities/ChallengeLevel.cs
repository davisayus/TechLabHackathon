using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ChallengeLevel
    {
        public ChallengeLevel()
        {
            Description ??= string.Empty;
        }

        [Key]
        public int LevelId { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public int NormalTime { get; set; }

        public virtual IEnumerable<Challenge> Challenges { get; set; } = null!;
    }
}
