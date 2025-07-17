using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Mapper;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository _walkRepo;

        public WalksController(IMapper mapper, IWalkRepository walkRepo)
        {
            this.mapper = mapper;
            _walkRepo = walkRepo;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn,   [FromQuery] string? filterQuery,
                                                     [FromQuery] string? sortBy,     [FromQuery] bool? isAscending,
                                                     [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var WalksDomain = await _walkRepo.GetAllWalksAsync(filterOn, filterQuery,sortBy,isAscending?? true,
                                                                pageNumber, pageSize );
            var WalkstDto = mapper.Map<List<WalkDto>>(WalksDomain);
            return Ok(WalkstDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepo.GetWalkByIdAsync(id);

            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }


        [HttpPost]
        [ValidationModelAttribute]
        public async Task<IActionResult> CreateWalk([FromBody]AddWalkRequestDto addWalkRequestDto)
        {
            
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                walkDomainModel = await _walkRepo.CreateNewWalkAsync(walkDomainModel);

                var walkDto = mapper.Map<WalkDto>(walkDomainModel);

                return Ok(walkDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidationModelAttribute]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            
                var walkDomain = mapper.Map<Walk>(updateWalkRequestDto);
                walkDomain = await _walkRepo.UpdateWalkAsync(id, walkDomain);

                if (walkDomain == null)
                {
                    return NotFound();
                }

                var walkDto = mapper.Map<WalkDto>(walkDomain);

                return Ok(walkDto);
           

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepo.DeleteWalkAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);

        }
    }
}
