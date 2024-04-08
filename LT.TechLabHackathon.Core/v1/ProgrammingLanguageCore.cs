using LT.TechLabHackathon.Core.v1.Generic;
using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Core.v1
{
    public class ProgrammingLanguageCore : GenericCore<ProgrammingLanguage, ProgrammingLanguageDto, ProgrammingLanguageCreateDto>, IGenericCore<ProgrammingLanguage, ProgrammingLanguageDto, ProgrammingLanguageCreateDto>
    {
        public ProgrammingLanguageCore(IGenericRepository<ProgrammingLanguage> repository, ILogger<ProgrammingLanguage> logger) : base("ProgrammingLanguageCoreG", repository, logger)
        {
        }
    }
}
