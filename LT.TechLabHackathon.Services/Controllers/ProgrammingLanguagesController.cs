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

namespace LT.TechLabHackathon.Services.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : GenericController<ProgrammingLanguage, ProgrammingLanguageDto, ProgrammingLanguageCreateDto>
    {
        private readonly IGenericCore<ProgrammingLanguage, ProgrammingLanguageDto, ProgrammingLanguageCreateDto> core; 
        public ProgrammingLanguagesController(IProgrammingLanguageRepository repository, ILogger<ProgrammingLanguage> logger) : base(new ProgrammingLanguageCore(repository, logger), repository, logger)
        {
            core = _core;
        }

        [HttpPost("compile")]
        public async Task<ActionResult<ResponseService<string>>> CompileCode([FromBody] RequestCompile requestCompile)
        {
            var compiler = new CompilerDotNet();
            var response = await compiler.Compile(requestCompile.Challenge, requestCompile.Code);
            return Ok(new ResponseService<string>(false,"Compiled successfuly",System.Net.HttpStatusCode.OK, response));
        }
    }
}
