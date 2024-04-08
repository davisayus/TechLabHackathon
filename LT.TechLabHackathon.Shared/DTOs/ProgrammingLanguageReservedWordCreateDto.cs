using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ProgrammingLanguageReservedWordCreateDto(string reservedWord)
    {
        public string ReservedWord { get; } = reservedWord;
    }
}
