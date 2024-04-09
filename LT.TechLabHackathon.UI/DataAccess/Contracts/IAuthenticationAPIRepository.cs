using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.Shared.Helpers;
using static LT.TechLabHackathon.Shared.DTOs.Records;

namespace LT.TechLabHackathon.UI.DataAccess.Contracts
{
    public interface IAuthenticationAPIRepository
    {
        Task<ResponseService<LoginResponseDto>> Login(LoginRequestDto loginRequest);
        Task<ResponseService<bool>> RequestDynamicPassword(string userName);
        Task<ResponseService<LoginResponseDto>> LoginDynamicPassword(LoginRequestDto loginRequest);
        Task<ResponseService<bool>> SetPassword(LoginRequestDto loginRequest);
        Task<ResponseService<bool>> ChangePassword(LoginChangePasswordDto loginRequest);
        Task<ResponseService<UserDto>> GetUserAuthenticated();
        Task<ResponseService<LoginResponseDto>> RenewToken(LoginRenewToken loginRenewToken);
    }
}
