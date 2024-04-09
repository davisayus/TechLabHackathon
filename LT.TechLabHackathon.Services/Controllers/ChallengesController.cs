using LT.TechLabHackathon.Core.v1.Generic;
using LT.TechLabHackathon.Core.v1.Providers;
using LT.TechLabHackathon.Core.v1;
using LT.TechLabHackathon.Domain.Contracts;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Services.Controllers.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LT.TechLabHackathon.Services.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChallengesController : GenericController<Challenge, ChallengeDto, ChallengeCreateDto>
    {
        private readonly IGenericCore<Challenge, ChallengeDto, ChallengeCreateDto> core;

        public ChallengesController(IChallengeRepository repository, ILogger<Challenge> logger) : base(new ChallengeCore(repository, logger), repository, logger)
        {
            core = _core;
        }
    }

}
