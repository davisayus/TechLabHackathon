using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class AuthUserKey
    {
        public AuthUserKey()
        {
            DynamicKey ??= string.Empty;    
        }

        [Key]
        public int AuthUserKeyId { get; set; }
        public int UserId { get; set; }
        public DateTime KeyCreation { get; set; }
        public DateTime KeyExpiration { get; set; }
        public string DynamicKey { get; set; }
        public int StatusId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
