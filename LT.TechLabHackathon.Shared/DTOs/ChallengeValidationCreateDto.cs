using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeValidationCreateDto
    {
        public ChallengeValidationCreateDto()
        {
            OutputValue ??= string.Empty;
        }

        public ChallengeValidationCreateDto(int challengeId, int parameterTypeId, string outputValue)
        {
            if (string.IsNullOrEmpty(outputValue))
                throw new ArgumentException($"'{nameof(outputValue)}' cannot be null or empty.", nameof(outputValue));

            ChallengeId = challengeId;
            ParameterTypeId = parameterTypeId;
            OutputValue = outputValue;
        }

        public int ChallengeId { get; set; }
        public int ParameterTypeId { get; set; }
        public string OutputValue { get; set; }
    }
}
