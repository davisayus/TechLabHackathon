﻿using LT.TechLabHackathon.Core.v1;
using LT.TechLabHackathon.Core.v1.Generic;
using LT.TechLabHackathon.Domain.Contracts;
using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Services.Controllers.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LT.TechLabHackathon.Shared.Helpers;

namespace LT.TechLabHackathon.Services.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : GenericController<User, UserDto, UserCreateDto>
    {
        private readonly IGenericCore<User, UserDto, UserCreateDto> core;
        public UsersController(IUserRepository repository, ILogger<User> logger) : base(new UserCore(repository, logger), repository, logger)
        {
            core = _core;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public override Task<ActionResult<ResponseService<UserDto>>> Add([FromBody] UserCreateDto createDto)
        {
            return base.Add(createDto);
        }
    }
}
