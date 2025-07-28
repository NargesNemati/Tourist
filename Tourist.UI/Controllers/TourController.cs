using Microsoft.AspNetCore.Mvc;
using Tourist.UI.Models;
using Tourist.UI.Models.Dto;
using Tourist.UI.Services;

namespace Tourist.UI.Controllers
{
    public class TourController : Controller
    {
        private readonly TourService tourService;
        private readonly ReviewService reviewService;

        public TourController(ReviewService reviewService, TourService tourService)
        {
            this.reviewService = reviewService;
            this.tourService = tourService;
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var tour = await tourService.GetTourByIdAsync(id);
            if (tour == null)
                return NotFound();

            var reviews = await reviewService.GetReviewsByTourIdAsync(id);

            var viewModel = new TourDetailsViewModel
            {
                Tour = tour,
                Reviews = reviews
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewDto reviewDto)
        {
            var token = HttpContext.Session.GetString("JWToken");
            Console.WriteLine($"TOKEN first: {token}");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");
            var result = await reviewService.AddReviewAsync(reviewDto);
            return RedirectToAction("Details", new { id = reviewDto.TourId });

        }
    }

}
