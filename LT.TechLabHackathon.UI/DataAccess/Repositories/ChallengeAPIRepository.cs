using Blazored.LocalStorage;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.UI.DataAccess.Contracts;
using LT.TechLabHackathon.UI.DataAccess.Repositories.Generic;

namespace LT.TechLabHackathon.UI.DataAccess.Repositories
{
    public class ChallengeAPIRepository : GenericAPIRepository<ChallengeDto, ChallengeCreateDto>, IChallengeAPIRepository
    {
        public ChallengeAPIRepository(HttpClient httpClient, ILogger<ChallengeDto> logger, ILocalStorageService localStorage) : base(httpClient, "challenges", "api/v1", logger, localStorage)
        {
        }
    }
}
