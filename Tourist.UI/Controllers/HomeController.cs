using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tourist.UI.Models;
using Tourist.UI.Services;

namespace Tourist.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TourService tourService;

        public HomeController(ILogger<HomeController> logger, TourService tourService)
        {
            _logger = logger;
            this.tourService = tourService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await tourService.GetAllToursAsync();
            return View(tours);
        }
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var tour = await tourService.GetTourByIdAsync(id);
        //    if (tour == null)
        //        return NotFound();

        //    return View(tour);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
