using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeLanguageSignatureDto
    {
        public ChallengeLanguageSignatureDto()
        {
            Signature ??= string.Empty; 
        }

        public ChallengeLanguageSignatureDto(int languageSignatureId, int challengeId, int programmingLanguageId, string signature)
        {
            if (string.IsNullOrEmpty(signature))
                throw new ArgumentException($"'{nameof(signature)}' cannot be null or empty.", nameof(signature));

            LanguageSignatureId = languageSignatureId;
            ChallengeId = challengeId;
            ProgrammingLanguageId = programmingLanguageId;
            Signature = signature;
        }

        public int LanguageSignatureId { get; set; }
        public int ChallengeId { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Signature { get; set; }

    }
}
