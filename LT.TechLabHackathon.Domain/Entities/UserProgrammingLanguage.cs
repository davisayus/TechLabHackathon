using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class UserProgrammingLanguage
    {

        [Key]
        public int UserProgrammingLanguageId { get; set; }
        public int UserId { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; } = null!;
    }
}
