using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using static LT.TechLabHackathon.Shared.DTOs.Records;

namespace LT.TechLabHackathon.UI.Components.Login
{
    public partial class Login
    {
        [Parameter] public EventCallback<LoginResponseDto> OnLoginSuccess { get; set; }
        [Parameter] public EventCallback<string> OnLoginError { get; set; }

        string userName = "";
        string password = "";
        bool rememberMe = true;
        bool doubleFactorRequired = false;
        bool loggingIn = false;

        private async Task OnLogin(LoginArgs args, string name)
        {
            loggingIn = true;

            var responseLogin = await _authRepository.Login(new LoginRequestDto(args.Username, args.Password));
            if (responseLogin is null || responseLogin.HasError)
                await OnLoginError.InvokeAsync(responseLogin?.Message);
            else
                await OnLoginSuccess.InvokeAsync(responseLogin.Content);

            loggingIn = false;
        }

        void OnRegister(string name)
        {
            navigationManager.NavigateTo("register");
        }

        void OnResetPassword(string value, string name)
        {
            Console.WriteLine($"{name} -> ResetPassword for user: {value}");
        }
    }
}
