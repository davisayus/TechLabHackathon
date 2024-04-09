using LT.TechLabHackathon.Shared.DTOs;

namespace LT.TechLabHackathon.UI.Providers.Auth
{
    public interface ILoginService
    {
        Task Login(UserDto user, string token);
        Task Logout();
    }
}
