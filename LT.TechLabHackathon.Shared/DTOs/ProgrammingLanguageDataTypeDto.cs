using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.DTOs
{
    public class ProgrammingLanguageDataTypeDto
    {
        public ProgrammingLanguageDataTypeDto(int programmingLanguageDataTypeId, int dataTypeId, int programmingLanguageId)
        {
            ProgrammingLanguageDataTypeId = programmingLanguageDataTypeId;
            DataTypeId = dataTypeId;
            ProgrammingLanguageId = programmingLanguageId;
        }

        public ProgrammingLanguageDataTypeDto()
        {
            DataType ??= new();   
        }

        public int ProgrammingLanguageDataTypeId { get; set; }
        public int DataTypeId { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public GeneralDataTypeDto DataType { get; set; } = null!;
    }
}
