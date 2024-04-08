using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ChallengeInputParameter
    {
        public ChallengeInputParameter()
        {
            InputValue ??= string.Empty;
        }

        [Key]
        public int InputParameterId { get; set; }
        public int ValidationId { get; set; }
        public int Sequence { get; set; }
        public string InputValue { get; set; }

        public virtual ChallengeValidation Validation { get; set; } = null!;

    }
}
