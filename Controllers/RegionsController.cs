using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.Dto;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();
            //return DTO regions
            var regionsDto = mapper.Map<List<Models.Dto.Region>>(regions);
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegion")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region = await regionRepository.GetAsyncRegion(id);
            if(region == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<Models.Dto.Region>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion(AddRegion addRegion)
        {
            var region = new Models.Domains.Region()
            {
                Code = addRegion.Code,
                Area = addRegion.Area,
                Lat = addRegion.Lat,
                Long = addRegion.Long,
                Name = addRegion.Name,
                Population = addRegion.Population
            };
            var newRegion = await regionRepository.AddRegion(region);

            var regionDto = new Models.Dto.Region()
            {
                Id = newRegion.Id,
                Code = newRegion.Code,
                Area = newRegion.Area,
                Lat = newRegion.Lat,
                Long = newRegion.Long,
                Name = newRegion.Name,
                Population = newRegion.Population
            };

            return CreatedAtAction(nameof(GetRegion), new { id = regionDto.Id }, regionDto);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var regionDel  = await regionRepository.DeleteRegion(id);
            // if null NotFound
            if(regionDel == null)
            {
                return NotFound();
            }
            // If not null convert response back to DTO
            var regionDto = new Models.Dto.Region()
            {
                Id = regionDel.Id,
                Code = regionDel.Code,
                Area = regionDel.Area,
                Lat = regionDel.Lat,
                Long = regionDel.Long,
                Name = regionDel.Name,
                Population = regionDel.Population
            };
            // return Ok response
            return Ok(regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id, [FromBody]Models.Dto.UpdateRegion region)
        {
            //convert the updateRegion dTo to region
            var regionUp = new Models.Domains.Region()
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //Get region from database
            var regionUpdate = await regionRepository.UpdateRegion(id, regionUp);
            // if null NotFound
            if (regionUpdate == null)
            {              
                return NotFound();
            }
            // If not null convert response back to DTO
            var regionUpe = new Models.Dto.Region()
            {
                Code = regionUpdate.Code,
                Area = regionUpdate.Area,
                Lat = regionUpdate.Lat,
                Long = regionUpdate.Long,
                Name = regionUpdate.Name,
                Population = regionUpdate.Population
            };

            return Ok(regionUpe);
        }
    }
}
