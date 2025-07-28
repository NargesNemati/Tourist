using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourist.API.Models.DTO;
using Tourist.API.Repositories;
using Tourist.Models.Domain;

namespace Tourist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await reviewRepository.GetAllAsync();
            return Ok(mapper.Map<List<ReviewDto>>(reviews));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var review = await reviewRepository.GetByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(mapper.Map<ReviewDto>(review));
        }
        [HttpGet("Tour/{id:guid}")]
        public async Task<IActionResult> GetByTourId(Guid id)
        {
            var reviews = await reviewRepository.GetByTourIdAsync(id);
            if (reviews == null) return NotFound();
            return Ok(mapper.Map<List<ReviewDto>>(reviews));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] AddReviewRequestDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            request.UserId = userId;
            var review = mapper.Map<Review>(request);

            review = await reviewRepository.CreateAsync(review);

            var dto = mapper.Map<ReviewDto>(review);

            return CreatedAtAction(nameof(GetById), new { id = review.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewRequestDto request)
        {
            var review = mapper.Map<Review>(request);

            var updated = await reviewRepository.UpdateAsync(id, review);
            if (updated == null) return NotFound();
            return Ok(mapper.Map<ReviewDto>(review));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await reviewRepository.DeleteAsync(id);
            if (deleted == null) return NotFound();

            return NoContent();
        }
    }
}

