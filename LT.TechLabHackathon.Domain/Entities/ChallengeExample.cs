using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ChallengeExample
    {
        public ChallengeExample()
        {
            Explanation ??= string.Empty;            
        }

        [Key]
        public int ExampleId { get; set; }
        public int ChallengeId { get; set; }
        public int ValidationId { get; set; }
        public string Explanation { get; set; }

        public virtual Challenge Challenge { get; set; } = null!;

    }
}
