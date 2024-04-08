using LT.TechLabHackathon.Core.v1.Generic;
using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Core.v1
{
    public class UserCore : GenericCore<User, UserDto, UserCreateDto>, IGenericCore<User, UserDto, UserCreateDto>
    {
        public UserCore(IGenericRepository<User> repository, ILogger<User> logger) : base("UserCore", repository, logger)
        {
        }
    }
}
