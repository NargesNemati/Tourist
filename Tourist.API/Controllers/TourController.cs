using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using System.Text.Json;
using Tourist.API.Data;
using Tourist.API.Models.DTO;
using Tourist.Models.Domain;
using Tourist.Models.DTO;
using Tourist.Repositories;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly TouristDbContext dbContext;
        private readonly ITourRepository tourRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TourController> logger;
        public TourController(TouristDbContext dbContext, ITourRepository tourRepository,
            IMapper mapper, ILogger<TourController> logger)
        {
            this.dbContext = dbContext;
            this.tourRepository = tourRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger.LogInformation("GetAll Regions action metod invoked");
                var tourDomain = await tourRepository.GetAllAsync();

                logger.LogInformation($"Finished GetAll Regions request with data: {JsonSerializer.Serialize(tourDomain)}");
                return Ok(mapper.Map<List<TourDto>>(tourDomain));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var tourDomain = await tourRepository.GetByIdAsync(id);
            if (tourDomain == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<TourDto>(tourDomain));
        }

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRequestToursDto addTourRequestDto)
        {

            var tourDomainModel = mapper.Map<Tour>(addTourRequestDto);

            tourDomainModel = await tourRepository.CreateAsync(tourDomainModel);

            var regionDto = mapper.Map<TourDto>(tourDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTourRequestDto updateTourRequestDto)
        {

            var tourDomainModel = mapper.Map<Tour>(updateTourRequestDto);
            tourDomainModel = await tourRepository.UpdateAsync(id, tourDomainModel);
            if (tourDomainModel == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<TourDto>(tourDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var tourDomainModel = await tourRepository.DeleteAsync(id);

            if (tourDomainModel == null)
            {
                return NotFound();
            }

            //return deleted region
            return Ok(mapper.Map<TourDto>(tourDomainModel));
        }
    }
}
