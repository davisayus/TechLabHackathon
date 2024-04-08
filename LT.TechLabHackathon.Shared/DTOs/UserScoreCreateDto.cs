using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserScoreCreateDto
    {
        public UserScoreCreateDto(int userId, int score, int unlockedChallenges)
        {
            UserId = userId;
            Score = score;
            UnlockedChallenges = unlockedChallenges;
        }

        public int UserId { get; set; }
        public int Score { get; set; }
        public int UnlockedChallenges { get; set; }
    }
}
