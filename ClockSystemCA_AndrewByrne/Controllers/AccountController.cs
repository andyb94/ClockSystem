using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ClockSystemCA_AndrewByrne.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<HomeController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            // ? for potential nulls, ?? incase null exist default to false
            // Redirect if user is already auth
            if ((User?.Identity?.IsAuthenticated ?? false) == true)
            {
                return RedirectToAction("Index", "Clocking");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Validating user
            var user = _accountService.ValidateUser(email, password);
            if (user != null) 
            {
                // User is verified so creating cookie
                var claims = new List<Claim>
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.RoleName)
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the user using cookie, authentication will be used on all other pages
                // stores the cookie on browser, cookie is auto sent back on ever request
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Clocking");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            // Index is the login page
            return View("Index");
        }

        // Logout of system and remove cookie
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // return to Login page once logged out
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
