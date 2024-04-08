using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class User
    {
        public User()
        {
            Name ??= string.Empty;
            Email ??= string.Empty;
            Picture ??= string.Empty;
            PhoneNumber ??= string.Empty;
            Password ??= string.Empty;
        }

        [Key]
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool AuthDoublefactor { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual IEnumerable<UserChallenge> UserChallenges { get; set; } = null!;
        public virtual IEnumerable<UserChallengeHistory> UserChallengeHistories { get; set; } = null!;
        public virtual IEnumerable<UserScore> UserScores { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;

    }
}
