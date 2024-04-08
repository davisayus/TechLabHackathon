using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeLanguageSignatureCreateDto
    {
        public ChallengeLanguageSignatureCreateDto()
        {
            Signature ??= string.Empty;
        }

        public ChallengeLanguageSignatureCreateDto(int challengeId, int programmingLanguageId, string signature)
        {
            if (string.IsNullOrEmpty(signature))
                throw new ArgumentException($"'{nameof(signature)}' cannot be null or empty.", nameof(signature));

            ChallengeId = challengeId;
            ProgrammingLanguageId = programmingLanguageId;
            Signature = signature;
        }

        public int ChallengeId { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Signature { get; set; }

    }
}
