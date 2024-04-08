using Blazored.LocalStorage;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.UI.DataAccess.Contracts;
using LT.TechLabHackathon.UI.DataAccess.Repositories.Generic;

namespace LT.TechLabHackathon.UI.DataAccess.Repositories
{
    public class UserAPIRepository : GenericAPIRepository<UserDto, UserCreateDto>, IUserAPIRepository
    {
        public UserAPIRepository(HttpClient httpClient, ILogger<UserDto> logger, ILocalStorageService localStorage) : base(httpClient, "users", "api/v1", logger, localStorage)
        {
        }


    }
}
