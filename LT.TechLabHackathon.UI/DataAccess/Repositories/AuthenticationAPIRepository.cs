using Blazored.LocalStorage;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.Shared.Helpers;
using LT.TechLabHackathon.UI.DataAccess.Contracts;
using System.Net.Http.Json;
using static LT.TechLabHackathon.Shared.DTOs.Records;

namespace LT.TechLabHackathon.UI.DataAccess.Repositories
{
    public class AuthenticationAPIRepository : IAuthenticationAPIRepository
    {
        protected readonly HttpClient _httpClient;
        protected readonly ErrorHandler<UserDto> _errorHandler;
        protected readonly ILocalStorageService _localStorage;

        public string Controller { get; }
        public string RoutePath { get; set; }

        public AuthenticationAPIRepository(HttpClient httpClient, ILogger<UserDto> logger, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _errorHandler = new ErrorHandler<UserDto>(logger);
            _localStorage = localStorage;

            Controller = "auths";
            RoutePath = "api/v1";
        }

        public async Task<ResponseService<LoginResponseDto>> Login(LoginRequestDto loginRequest)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync<LoginRequestDto>($"{RoutePath}/{Controller}/login/", loginRequest);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<LoginResponseDto>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error<LoginResponseDto>(ex, "Login", new LoginResponseDto(string.Empty, new UserDto()));
            }
        }

        public async Task<ResponseService<LoginResponseDto>> LoginDynamicPassword(LoginRequestDto loginRequest)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync<LoginRequestDto>($"{RoutePath}/{Controller}/logindynamic/", loginRequest);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<LoginResponseDto>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error<LoginResponseDto>(ex, "LoginDynamicPassword", new LoginResponseDto(string.Empty, new UserDto()));
            }
        }

        public async Task<ResponseService<bool>> RequestDynamicPassword(string userName)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync<string>($"{RoutePath}/{Controller}/requestdynamicpassword/{userName}",string.Empty);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<bool>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error<bool>(ex, "Login", false);
            }
        }

        public async Task<ResponseService<bool>> SetPassword(LoginRequestDto loginRequest)
        {
            try
            {
                var httpResponse = await _httpClient.PatchAsJsonAsync<LoginRequestDto>($"{RoutePath}/{Controller}/setpassword/", loginRequest);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<bool>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "SetPassword", false);
            }
        }
        
        public async Task<ResponseService<bool>> ChangePassword(LoginChangePasswordDto loginRequest)
        {
            try
            {
                var httpResponse = await _httpClient.PatchAsJsonAsync<LoginChangePasswordDto>($"{RoutePath}/{Controller}/changepassword/", loginRequest);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<bool>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "SetPassword", false);
            }
        }

        public async Task<ResponseService<UserDto>> GetUserAuthenticated()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseService<UserDto>>($"{RoutePath}/{Controller}/userauthenticated/");
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error<UserDto>(ex, "GetAllAsync", new ());
            }
        }

        public async Task<ResponseService<LoginResponseDto>> RenewToken(LoginRenewToken loginRequest)
        {
            try
            {
                var httpResponse = await _httpClient.PostAsJsonAsync<LoginRenewToken>($"{RoutePath}/{Controller}/renewtoken/", loginRequest);
                var response = await httpResponse.Content.ReadFromJsonAsync<ResponseService<LoginResponseDto>>();
                if (response == null || response.HasError)
                    throw new Exception(response == null ? "Service did not return a response" : response.Message);

                return response;
            }
            catch (Exception ex)
            {
                return _errorHandler.Error<LoginResponseDto>(ex, "RenewToken", new LoginResponseDto(string.Empty, new UserDto()));
            }
        }
    }
}
