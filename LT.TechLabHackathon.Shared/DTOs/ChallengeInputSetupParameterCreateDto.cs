using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeInputSetupParameterCreateDto
    {
        public ChallengeInputSetupParameterCreateDto(int challengeId, int sequence, string parameterName, int dataTypeId)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentException($"'{nameof(parameterName)}' cannot be null or empty.", nameof(parameterName));

            ChallengeId = challengeId;
            Sequence = sequence;
            ParameterName = parameterName;
            DataTypeId = dataTypeId;
        }

        public ChallengeInputSetupParameterCreateDto()
        {
            ParameterName ??= string.Empty;
        }

        public int ChallengeId { get; set; }
        public int Sequence { get; set; }
        public string ParameterName { get; set; }
        public int DataTypeId { get; set; }
    }
}
