using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.UI.DataAccess.Contracts.Generic;

namespace LT.TechLabHackathon.UI.DataAccess.Contracts
{
    public interface IUserAPIRepository: IGenericAPIRepository<UserDto, UserCreateDto>
    {
    }
}
