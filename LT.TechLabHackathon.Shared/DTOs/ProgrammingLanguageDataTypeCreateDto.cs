using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ProgrammingLanguageDataTypeCreateDto
    {
        public ProgrammingLanguageDataTypeCreateDto(int dataTypeId, int programmingLanguageId)
        {
            DataTypeId = dataTypeId;
            ProgrammingLanguageId = programmingLanguageId;
        }

        public int DataTypeId { get; set; }
        public int ProgrammingLanguageId { get; set; }
    }
}
