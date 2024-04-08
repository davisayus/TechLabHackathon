using LT.TechLabHackathon.Core.v1.Generic;
using LT.TechLabHackathon.Domain.Contracts.Generic;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace LT.TechLabHackathon.Services.Controllers.Generic
{
    public abstract class GenericController<T, Q, C>: ControllerBase where T : class, new() where Q : class, new() where C : class, new()   
    {
        protected IGenericCore<T, Q, C> _core;

        public GenericController(IGenericCore<T, Q, C> core, IGenericRepository<T> repository, ILogger<T> logger)
        {
            _core = core;
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ResponseService<Q>>> GetByIdAsync(int id)
        {
            var response = await _core.GetByIdAsync(id);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseService<IEnumerable<Q>>>> GetAllAsync()
        {
            var response = await _core.GetAllAsync();
            return StatusCode((int)response.StatusHttp, response);
        }

        //[HttpPost("filters")]
        //public async Task<ActionResult<ResponseService<IEnumerable<Q>>>> GetByFilterAsync([FromBody] Dictionary<string, object> filters)
        //{
        //    //var response = await _core.GetAllWithFilters(filters);
        //    return StatusCode((int)response.StatusHttp, response);
        //}

        [HttpPost("create")]
        public async Task<ActionResult<ResponseService<Q>>> Add([FromBody] C createDto)
        {
            var response = await _core.AddAsync(createDto);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<ResponseService<bool>>> Update([FromBody] C updateDto, int id)
        {
            var response = await _core.UpdateAsync(updateDto, id);
            return StatusCode((int)response.StatusHttp, response);
        }

    }
}
