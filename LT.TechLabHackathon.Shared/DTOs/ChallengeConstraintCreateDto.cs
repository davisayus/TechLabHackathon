using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeConstraintCreateDto
    {
        public ChallengeConstraintCreateDto(string description, int challengeId)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));
            
            Description = description;
            ChallengeId = challengeId;
        }

        public ChallengeConstraintCreateDto()
        {
            Description ??= string.Empty;   
        }

        public int ChallengeId { get; set; }
        public string Description { get; set; }
    }
}
