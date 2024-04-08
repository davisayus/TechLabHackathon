using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class UserProgrammingLanguageCreateDto
    {
        public UserProgrammingLanguageCreateDto(int userId, int programmingLanguageId)
        {
            UserId = userId;
            ProgrammingLanguageId = programmingLanguageId;
        }

        public UserProgrammingLanguageCreateDto()
        {
            
        }

        public int UserId { get; set; }
        public int ProgrammingLanguageId { get; set; }
    }
}
