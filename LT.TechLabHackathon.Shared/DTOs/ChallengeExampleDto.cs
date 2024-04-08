using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeExampleDto
    {
        public int ExampleId { get; set; }
        public int ChallengeId { get; set; }
        public int ValidationId { get; set; }
        public string Explanation { get; set; }

        public ChallengeExampleDto()
        {
            Explanation ??= string.Empty;
        }

        public ChallengeExampleDto(int exampleId, int challengeId, int validationId, string explanation)
        {
            if (string.IsNullOrEmpty(explanation))
                throw new ArgumentException($"'{nameof(explanation)}' cannot be null or empty.", nameof(explanation));

            ExampleId = exampleId;
            ChallengeId = challengeId;
            ValidationId = validationId;
            Explanation = explanation;
        }
    }
}
