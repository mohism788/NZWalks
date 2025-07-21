using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepo, IMapper mapper, ILogger<RegionsController> logger)
        {
            _regionRepo = regionRepo;
            _mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllRegions([FromQuery] string? filterOn,[FromQuery] string? filterQuery,
                                                       [FromQuery] bool? isAscending)
        {

            logger.LogInformation("GetAllRegions action message was invoked");
            var regionsDomain = await _regionRepo.GetAllRegionsAsync(filterOn, filterQuery, isAscending);

            var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);
            logger.LogInformation($"Finished GetAllRegions request with data {JsonSerializer.Serialize(regionsDomain)}");
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepo.GetRegionByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
           
           var regionDto = _mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }

        [HttpPost]
        [ValidationModelAttribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            
                var regionDomain = _mapper.Map<Region>(addRegionRequestDto);

                regionDomain = await _regionRepo.CreateNewRegionAsync(regionDomain);

                var regionDto = _mapper.Map<RegionDto>(regionDomain);

                return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
           
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidationModelAttribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id,[FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
                var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

                regionDomainModel = await _regionRepo.UpdateRegionAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
            
        }




        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepo.DeleteRegionAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDto);

        }
    }
}
