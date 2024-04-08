using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeExampleCreateDto
    {
        public int ChallengeId { get; set; }
        public int ValidationId { get; set; }
        public string Explanation { get; set; }

        public ChallengeExampleCreateDto()
        {
            Explanation ??= string.Empty;
        }

        public ChallengeExampleCreateDto(int challengeId, int validationId, string explanation)
        {
            if (string.IsNullOrEmpty(explanation))
                throw new ArgumentException($"'{nameof(explanation)}' cannot be null or empty.", nameof(explanation));

            ChallengeId = challengeId;
            ValidationId = validationId;
            Explanation = explanation;
        }
    }
}
