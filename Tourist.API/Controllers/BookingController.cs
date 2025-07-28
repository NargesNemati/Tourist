using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Tourist.API.Models.DTO;
using Tourist.API.Repositories;
using Tourist.Models.Domain;

namespace Tourist.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBookingRepository bookingRepository;
        public BookingController(IMapper mapper, IBookingRepository bookingRepository)
        {
            this.mapper = mapper;
            this.bookingRepository = bookingRepository;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Add([FromBody] AddBookingRequestDto addBookingRequestDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            addBookingRequestDto.UserId = userId;
            var bookingDomainModel = mapper.Map<Booking>(addBookingRequestDto);
            await bookingRepository.CreateAsync(bookingDomainModel);
            return Ok(mapper.Map<BookingDto>(bookingDomainModel));
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetBookingsByUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var bookings = await bookingRepository.GetByUserIdAsync(userId);
            var bookingDtos = mapper.Map<List<BookingDto>>(bookings);
            return Ok(bookingDtos);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookingsDomainModel = await bookingRepository.GetAllAsync();
            return Ok(mapper.Map<List<BookingDto>>(bookingsDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var bookingDomainModel = await bookingRepository.GetByIdAsync(id);
            return Ok(mapper.Map<BookingDto>(bookingDomainModel));
        }



        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var bookingDeleteDomainModel = await bookingRepository.DeleteAsync(id);
            if (bookingDeleteDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookingDto>(bookingDeleteDomainModel));
        }
    }
}
