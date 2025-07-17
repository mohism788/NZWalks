using AutoMapper;
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

        public RegionsController(IRegionRepository regionRepo, IMapper mapper)
        {
            _regionRepo = regionRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regionsDomain = await _regionRepo.GetAllRegionsAsync();

            var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
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
