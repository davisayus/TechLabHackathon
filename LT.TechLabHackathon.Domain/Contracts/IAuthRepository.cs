using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Contracts
{
    public interface IAuthRepository
    {
        Task<AuthUserKey> GetUserDynamicKey(int userId);
        Task<User> GetUserByEmail(string user);
        Task<AuthUserKey> AddUserDynamicKey(AuthUserKey userToken);
        Task<bool> UpdateUserDynamicKey(AuthUserKey userKey);
        Task<bool> UpdatePasswordUser(int userId, string password);
    }
}
