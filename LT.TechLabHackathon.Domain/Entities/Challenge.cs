using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class Challenge
    {
        public Challenge()
        {
            Description ??= string.Empty;
            Title ??= string.Empty;
            MethodName ??= string.Empty;
        }

        [Key]
        public int ChallengeId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int LevelId { get; set; }

        public string MethodName { get; set; }
        public int InputParameters { get; set; }
        public int ResultDataTypeId { get; set; }

        public virtual ChallengeLevel Level { get; set; } = null!;
        public virtual GeneralDataType DataType { get; set; } = null!;
        public virtual IEnumerable<ChallengeConstraint> Constraints { get; set; } = null!;
        public virtual IEnumerable<ChallengeLanguageSignature> LanguageSignatures { get; set; } = null!;
        public virtual IEnumerable<ChallengeValidation> Validations { get; set; } = null!;
        public virtual IEnumerable<ChallengeExample> Examples { get; set; } = null!;
        public virtual IEnumerable<ChallengeInputSetupParameter> InputSetupParameters { get; set; }

    }
}
