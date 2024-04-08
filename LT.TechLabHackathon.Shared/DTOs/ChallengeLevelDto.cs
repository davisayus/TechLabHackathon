using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ChallengeLevelDto
    {
        public int LevelId { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }

        public ChallengeLevelDto()
        {
            Description ??= string.Empty;
        }

        public ChallengeLevelDto(int levelId, string description, int score)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));

            LevelId = levelId;
            Description = description;
            Score = score;
        }

    }
}
