using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserScoreDto
    {
        public UserScoreDto(int userScoreId, int userId, int score, int unlockedChallenges)
        {
            UserScoreId = userScoreId;
            UserId = userId;
            Score = score;
            UnlockedChallenges = unlockedChallenges;
        }

        public UserScoreDto()
        {
        }

        public int UserScoreId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int UnlockedChallenges { get; set; }

        public UserDto User { get; set; } = null!;
    }
}
