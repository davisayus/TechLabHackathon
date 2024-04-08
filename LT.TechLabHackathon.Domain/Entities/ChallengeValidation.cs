using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ChallengeValidation
    {
        public ChallengeValidation()
        {
            OutputValue ??= string.Empty;
        }

        [Key]
        public int ValidationId { get; set; }
        public int ChallengeId { get; set; }
        public string OutputValue { get; set; }

        public virtual Challenge Challenge { get; set; } = null!;
        public virtual IEnumerable<ChallengeInputParameter> InputParameters { get; set; } = null!;
    }
}
