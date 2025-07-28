using Microsoft.AspNetCore.Mvc;
using Tourist.UI.Models.Dto;
using Tourist.UI.Services;

public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (result)
            return RedirectToAction("Login");

        ModelState.AddModelError("", "ثبت‌نام ناموفق بود");
        return View(dto);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (result != null)
        {
            // ذخیره JWT در Session
            HttpContext.Session.SetString("JWToken", result.JwtToken);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "ورود ناموفق بود");
        return View(dto);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
