using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ProgrammingLanguageReservedWord
    {
        public ProgrammingLanguageReservedWord()
        {
            ReservedWord ??= string.Empty;
        }

        [Key]
        public int ProgrammingLanguageReservedWordId { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string ReservedWord { get; set; }

        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; } = null!;
    }
}
