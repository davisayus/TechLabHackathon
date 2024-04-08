using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public sealed class ProgrammingLanguageReservedWordDto(int programmingLanguageReservedWordId, string reservedWord)
    {
        public int ProgrammingLanguageReservedWordId { get; set; } = programmingLanguageReservedWordId;
        public string ReservedWord { get; set; } = reservedWord;
    }
}
