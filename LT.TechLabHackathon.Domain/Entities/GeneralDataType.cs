using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class GeneralDataType
    {
        public GeneralDataType()
        {
            Description ??= string.Empty;
            Name ??= string.Empty;
        }

        [Key]
        public int DataTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<ChallengeInputSetupParameter> InputSetupParameters { get; set; } = null!;
        public virtual IEnumerable<Challenge> Challenges { get; set; } = null!;
        public virtual IEnumerable<ProgrammingLanguageDataType> ProgrammingLanguageDataTypes { get; set; } = null!;
    }
}
