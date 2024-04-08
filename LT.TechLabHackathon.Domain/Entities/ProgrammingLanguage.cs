using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ProgrammingLanguage
    {
        public ProgrammingLanguage()
        {
            Description ??= string.Empty;
            Title ??= string.Empty;
        }

        [Key]
        public int ProgrammingLanguageId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Version { get; set; }

        public virtual IEnumerable<ProgrammingLanguageReservedWord> ReservedWords { get; set; } = null!;
        public virtual IEnumerable<ProgrammingLanguageDataType> DataTypes { get; set; } = null!;

    }
}
