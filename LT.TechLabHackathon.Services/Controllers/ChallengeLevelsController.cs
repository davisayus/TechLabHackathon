using LT.TechLabHackathon.Core.v1;
using LT.TechLabHackathon.Core.v1.Generic;
using LT.TechLabHackathon.Domain.Contracts;
using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Services.Controllers.Generic;
using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LT.TechLabHackathon.Services.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChallengeLevelsController : GenericController<ChallengeLevel, ChallengeLevelDto, ChallengeLevelCreateDto>
    {
        private readonly IGenericCore<ChallengeLevel, ChallengeLevelDto, ChallengeLevelCreateDto> core;
        public ChallengeLevelsController(IChallengeLevelRepository repository, ILogger<ChallengeLevel> logger) : base(new ChallengeLevelCore(repository, logger), repository, logger)
        {
            core = _core;
        }
    }
}
