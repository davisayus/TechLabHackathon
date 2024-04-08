using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.UI.DataAccess.Contracts.Generic;
using static LT.TechLabHackathon.Shared.DTOs.Records;

namespace LT.TechLabHackathon.UI.DataAccess.Contracts
{
    public interface IProgrammingLanguageAPIRepository: IGenericAPIRepository<ProgrammingLanguageDto, ProgrammingLanguageCreateDto>
    {
        Task<string> Compile(RequestCompile requestCompile);
    }
}
