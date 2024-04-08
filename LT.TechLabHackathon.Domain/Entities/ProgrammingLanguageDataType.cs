using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Entities
{
    public class ProgrammingLanguageDataType
    {
        [Key]
        public int ProgrammingLanguageDataTypeId { get; set; }
        public int DataTypeId { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public ProgrammingLanguage ProgrammingLanguage { get; set; } = null!;
        public GeneralDataType DataType { get; set; } = null!;
    }
}
