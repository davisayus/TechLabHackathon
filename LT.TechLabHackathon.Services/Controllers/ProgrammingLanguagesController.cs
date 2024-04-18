using LT.TechLabHackathon.Core.v1;
using LT.TechLabHackathon.Core.v1.Contracts;
using LT.TechLabHackathon.Core.v1.Generic;
using LT.TechLabHackathon.Core.v1.Providers;
using LT.TechLabHackathon.Domain.Contracts;
using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Services.Controllers.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static LT.TechLabHackathon.Shared.DTOs.Records;
using LT.TechLabHackathon.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace LT.TechLabHackathon.Services.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : GenericController<ProgrammingLanguage, ProgrammingLanguageDto, ProgrammingLanguageCreateDto>
    {
        private readonly IGenericCore<ProgrammingLanguage, ProgrammingLanguageDto, ProgrammingLanguageCreateDto> core;
        private readonly ICompiler _compiler;

        public ProgrammingLanguagesController(IProgrammingLanguageRepository repository, ILogger<ProgrammingLanguage> logger, ICompiler compiler) : base(new ProgrammingLanguageCore(repository, logger), repository, logger)
        {
            core = _core;
            _compiler = compiler;
        }

        [HttpPost("compile")]
        public async Task<ActionResult<ResponseService<(IEnumerable<object> ResultList,long MemoryUse)>>> CompileCode([FromBody] RequestCompile requestCompile)
        {
            var response = await _compiler.CompileAsync(requestCompile.Challenge, requestCompile.Code);
            return Ok(new ResponseService<(IEnumerable<object> ResultList, long MemoryUse)>(false,"Compiled successfuly",System.Net.HttpStatusCode.OK, response));
        }
    }
}
