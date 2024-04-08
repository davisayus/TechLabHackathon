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


        string userName = "techlab@gmail.com";
        string password = "admin";
        bool rememberMe = true;

        bool doubleFactorRequired = false;

        private async Task OnLogin(LoginArgs args, string name)
        {
            var responseLogin = await _authRepository.Login(new LoginRequestDto(args.Username, args.Password));
            if (responseLogin == null || responseLogin.HasError)
                await OnLoginError.InvokeAsync(responseLogin?.Message);
            else
                await OnLoginSuccess.InvokeAsync(responseLogin.Content);

            Console.WriteLine($"{name} -> Username: {args.Username}, password: {args.Password}, remember me: {args.RememberMe}");
        }

        void OnRegister(string name)
        {
            navigationManager.NavigateTo("register");
            Console.WriteLine($"{name} -> Register");
        }

        void OnResetPassword(string value, string name)
        {
            Console.WriteLine($"{name} -> ResetPassword for user: {value}");
        }
    }
}
