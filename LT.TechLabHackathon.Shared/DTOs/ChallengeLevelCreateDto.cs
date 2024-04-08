using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeLevelCreateDto
    {
        public string Description { get; set; }
        public int Score { get; set; }

        public ChallengeLevelCreateDto()
        {
            Description ??= string.Empty;
        }

        public ChallengeLevelCreateDto(string description, int score)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            Description = description;
            Score = score;
        }
    }
}
