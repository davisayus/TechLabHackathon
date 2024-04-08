using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Domain.Contracts
{
    public interface IUserRepository: IGenericRepository<User>
    {
    }
}
