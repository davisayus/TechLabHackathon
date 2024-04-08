using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserProgrammingLanguageDto
    {
        public int UserProgrammingLanguageId { get; set; }
        public int UserId { get; set; }
        public int ProgrammingLanguageId { get; set; }
    }
}
