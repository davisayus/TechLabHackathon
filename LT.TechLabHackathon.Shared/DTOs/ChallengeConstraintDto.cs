using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeConstraintDto
    {
        public ChallengeConstraintDto(int constraintId, int challengeId, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            ConstraintId = constraintId;
            ChallengeId = challengeId;
            Description = description;
        }

        public ChallengeConstraintDto()
        {
            Description ??= string.Empty;
        }

        public int ConstraintId { get; set; }
        public int ChallengeId { get; set; }
        public string Description { get; set; }

    }
}
