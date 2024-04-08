using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace LT.TechLabHackathon.UI.Components.Login
{
    public partial class Welcome
    {

        public UserDto UserInfo { get; set; }

        public Welcome()
        {
            UserInfo = _collectionsShared?.UserInfo ?? new();
        }

        protected override async Task OnInitializedAsync()
        {
            if (_collectionsShared is null) return;

            if (_collectionsShared.UserInfo is null || _collectionsShared.UserInfo.UserId == 0)
            {
                var response = await _authRepository.GetUserAuthenticated();
                if (response != null)
                {
                    UserInfo = response.Content ?? new();
                    _collectionsShared.UserInfo = UserInfo;
                }
            }
            else 
                UserInfo = _collectionsShared.UserInfo;

            await base.OnInitializedAsync();
        }

        private async Task Logout()
        {
            await _loginService.Logout();
        }
    }
}
