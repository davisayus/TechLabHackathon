using Blazored.LocalStorage;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.Shared.Helpers;
using LT.TechLabHackathon.UI.DataAccess.Contracts;
using LT.TechLabHackathon.UI.DataAccess.Repositories.Generic;
using System.Net.Http.Json;
using static LT.TechLabHackathon.Shared.DTOs.Records;

namespace LT.TechLabHackathon.UI.DataAccess.Repositories
{
    public class ProgrammingLanguageAPIRepository: GenericAPIRepository<ProgrammingLanguageDto, ProgrammingLanguageCreateDto>, IProgrammingLanguageAPIRepository 
    {
        public ProgrammingLanguageAPIRepository(HttpClient httpClient, ILogger<ProgrammingLanguageDto> logger, ILocalStorageService localStorage) : base(httpClient, "ProgrammingLanguages", "api/v1", logger, localStorage)
        {
        }

        public async Task<string> Compile(RequestCompile requestCompile)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync<RequestCompile>($"{RoutePath}/{Controller}/compile/", requestCompile);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<string>>();
                if (response is null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response":string.Empty);

                return response.Content ?? string.Empty;
            }
            catch (Exception ex)
            {
                return  string.Empty;
            }
        }
    }
}
