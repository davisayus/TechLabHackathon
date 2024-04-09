using Blazored.LocalStorage;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.UI.Core;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http;
using System.Security.Claims;

namespace LT.TechLabHackathon.UI.Providers.Auth
{
    public class AuthenticationStateJWT : AuthenticationStateProvider, ILoginService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly CollectionsShared  _collectionsShared;

        public string AuthenticatedUserEmail { get; set; }
        public string AuthenticatedUserName { get; set; }

        private AuthenticationState AnonymousAuthentication => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public AuthenticationStateJWT(ILocalStorageService localStorageService, HttpClient httpClient, CollectionsShared collectionsShared)
        {
            _localStorage = localStorageService;    
            _httpClient = httpClient;
            _collectionsShared = collectionsShared;
            AuthenticatedUserEmail = string.Empty;
            AuthenticatedUserName = string.Empty;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await _collectionsShared.InitializedAsync(_localStorage);
            var token = _collectionsShared.CurrentToken;
            
            if (string.IsNullOrEmpty(token))
                return AnonymousAuthentication;
            else
                return BuildAuthenticationState(token);
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            httpClientHeaderAuthentication(token);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims;

            if (claims is null || !claims.Any()) 
                return AnonymousAuthentication;

            AuthenticatedUserEmail = claims.Where(c => c.Type == "email").FirstOrDefault(new Claim("email", "")).Value;
            AuthenticatedUserName = claims.Where(c => c.Type == "name").FirstOrDefault(new Claim("name", ""))!.Value;
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        public async Task Login(UserDto user, string token)
        {
            _collectionsShared.UserInfo = user;
            _collectionsShared.CurrentToken = token;
            await _collectionsShared.UpdateTokenAsync(_localStorage);

            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            _collectionsShared.CurrentToken = string.Empty;
            _collectionsShared.UserInfo = new UserDto();
            await _collectionsShared.ResetCollectionShared(_localStorage, CollectionsShared.EnCollectionShared.Token);

            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            NotifyAuthenticationStateChanged(Task.FromResult(AnonymousAuthentication));
        }

        private void httpClientHeaderAuthentication(string token)
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
        }
    }
}
