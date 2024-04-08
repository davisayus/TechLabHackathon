using Blazored.LocalStorage;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.UI.DataAccess.Contracts;
using LT.TechLabHackathon.UI.DataAccess.Repositories.Generic;

namespace LT.TechLabHackathon.UI.DataAccess.Repositories
{
    public class ChallengeLevelAPIRepository : GenericAPIRepository<ChallengeLevelDto, ChallengeLevelCreateDto>, IChallengeLevelAPIRepository
    {
        public ChallengeLevelAPIRepository(HttpClient httpClient, ILogger<ChallengeLevelDto> logger, ILocalStorageService localStorage) : base(httpClient, "challengelevels", "api/v1", logger, localStorage)
        {
        }
    }
}
