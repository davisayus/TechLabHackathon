using LT.TechLabHackathon.Shared.DTOs;
using static LT.TechLabHackathon.Shared.DTOs.Records;

namespace LT.TechLabHackathon.UI.Pages
{
    public partial class Home
    {
        private UserDto _userInfo;

        public Home()
        {
            _userInfo = new UserDto();    
        }

        protected override Task OnInitializedAsync()
        {

            return base.OnInitializedAsync();
        }

        private async Task OnLoginSuccess(LoginResponseDto loginResponse)
        {
            _userInfo = loginResponse.User;
            await _loginService.Login(loginResponse.User, loginResponse.Token);
            
            _notification.Notify(Radzen.NotificationSeverity.Success, "Login Successfully", "The user is logged in",3000);
        }

        private async Task OnLoginError(string messageError)
        {
            _notification.Notify(Radzen.NotificationSeverity.Error, "Login Error", messageError, 3000);
        }
    }
}
