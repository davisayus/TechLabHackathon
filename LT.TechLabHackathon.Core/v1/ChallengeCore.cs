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
    public class ChallengeCore : GenericCore<Challenge, ChallengeDto, ChallengeCreateDto>, IGenericCore<Challenge, ChallengeDto, ChallengeCreateDto>
    {
        public ChallengeCore(IGenericRepository<Challenge> repository, ILogger<Challenge> logger) : base("ChallengeCore", repository, logger)
        {
        }
    }
}
