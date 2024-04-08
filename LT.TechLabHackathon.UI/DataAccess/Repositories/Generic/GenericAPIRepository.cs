using Blazored.LocalStorage;
using LT.TechLabHackathon.Shared.Helpers;
using System.Net.Http.Json;

namespace LT.TechLabHackathon.UI.DataAccess.Repositories.Generic
{
    public abstract class GenericAPIRepository<Q, C> where Q : new ()
    {
        protected readonly HttpClient _httpClient;
        protected readonly ErrorHandler<Q> _errorHandler;
        protected readonly ILocalStorageService _localStorage;

        public string Controller { get; }
        public string RoutePath { get; set; }

        public GenericAPIRepository(HttpClient httpClient, string controller, string routePath, ILogger<Q> logger, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _errorHandler = new ErrorHandler<Q>(logger);
            _localStorage = localStorage;

            Controller = controller;
            RoutePath = routePath;
        }

        public async Task<ResponseService<IEnumerable<Q>>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseService<IEnumerable<Q>>>($"{RoutePath}/{Controller}/");
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "GetAllAsync", new List<Q>().AsEnumerable());
            }
        }

        public async Task<ResponseService<Q>> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseService<Q>>($"{RoutePath}/{Controller}/{id}");
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "GetAllAsync", new Q());
            }
        }

        public async Task<ResponseService<IEnumerable<Q>>> GetAllWithFiltersAsync(Dictionary<string, object> filters)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync<Dictionary<string, object>>($"{RoutePath}/{Controller}/filters/", filters);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<IEnumerable<Q>>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "GetAllAsync", new List<Q>().AsEnumerable());
            }
        }

        public async Task<ResponseService<Q>> Create(C createNew)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync<C>($"{RoutePath}/{Controller}/create/", createNew);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<Q>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "Create", new Q());
            }
        }

        public async Task<ResponseService<bool>> Update(C entityUpdated, int id)
        {
            try
            {
                var httpResponse = await _httpClient.PutAsJsonAsync<C>($"{RoutePath}/{Controller}/update/{id}", entityUpdated);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<bool>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "Update", false);
            }
        }


        public async Task InitializedAsync()
        {

            if (!_httpClient.DefaultRequestHeaders.Any(h => h.Key.Contains("Authorization")))
            {
                var token = await _localStorage.GetItemAsync<string>("LT.TechLabHackathon.Token");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
            }

            //_httpClient.DefaultRequestHeaders.Remove("Authorization");
        }
    }

}
