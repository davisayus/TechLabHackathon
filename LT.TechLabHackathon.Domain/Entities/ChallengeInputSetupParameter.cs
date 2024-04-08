using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ChallengeInputSetupParameter
    {

        public ChallengeInputSetupParameter()
        {
            ParameterName ??= string.Empty;
        }

        [Key]
        public int ChallengeInputSetupParameterId { get; set; }
        public int ChallengeId { get; set; }
        public int Sequence { get; set; }
        public string ParameterName { get; set; }
        public int DataTypeId { get; set; }

        public virtual Challenge Challenge { get; set; } = null!;
        public virtual GeneralDataType DataType { get; set; } = null!;

    }
}
