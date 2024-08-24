using BusinessLayer.EntitiesServices.AuthServices;
using BusinessLayer.ModelViews.AuthModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System.Security.Claims;

namespace LibraryTask.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (success, token, errorMessage) = await _authService.LoginAsync(model.Username, model.Password);

            if (!success)
            {
                ModelState.AddModelError("", errorMessage);
                return View(model);
            }

            // Save JWT in cookies for authorization
            HttpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.Strict
            });
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, model.Username)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home"); // Redirect to main screen after successful login
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.RegisterAsync(model.Username, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            return RedirectToAction("Login"); // Redirect to login after successful registration
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("JwtToken"); // Remove JWT token
            HttpContext.Session.Clear(); // Clear session data
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out of authentication
            return RedirectToAction("Login", "Auth");
        }
    }
}
