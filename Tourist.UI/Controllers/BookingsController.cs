using Microsoft.AspNetCore.Mvc;
using System;
using Tourist.UI.Services;

public class BookingsController : Controller
{
    private readonly BookingService _bookingService;

    public BookingsController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid tourId)
    {
        var token = HttpContext.Session.GetString("JWToken");
        Console.WriteLine($"TOKEN first: {token}");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login", "Auth");

        var success = await _bookingService.CreateBookingAsync(tourId);
        if (success)
            return RedirectToAction("MyBookings");

        TempData["Error"] = "رزرو انجام نشد.";
        return RedirectToAction("Details", "Tour", new { id = tourId });
    }

    [HttpGet]
    public async Task<IActionResult> MyBookings()
    {
        var token = HttpContext.Session.GetString("JWToken");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login", "Auth");

        var bookings = await _bookingService.GetUserBookingsAsync();
        return View(bookings);
    }
}
