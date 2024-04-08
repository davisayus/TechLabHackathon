using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ChallengeConstraint
    {

        public ChallengeConstraint()
        {
            Description ??= string.Empty;
        }

        [Key]
        public int ConstraintId { get; set; }
        public int ChallengeId { get; set; }
        public string Description { get; set; }

        public virtual Challenge Challenge { get; set; } = null!;
    }
}
