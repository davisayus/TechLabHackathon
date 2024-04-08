using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeValidationDto
    {
        public ChallengeValidationDto()
        {
            OutputValue ??= string.Empty;
        }

        public ChallengeValidationDto(int validationId, int challengeId, string outputValue)
        {
            if (string.IsNullOrEmpty(outputValue))
                throw new ArgumentException($"'{nameof(outputValue)}' cannot be null or empty.", nameof(outputValue));

            ValidationId = validationId;
            ChallengeId = challengeId;
            OutputValue = outputValue;
        }

        public int ValidationId { get; set; }
        public int ChallengeId { get; set; }
        public string OutputValue { get; set; }

        public IEnumerable<ChallengeInputParameterDto> InputParameters { get; set; } = null!;
    }
}
