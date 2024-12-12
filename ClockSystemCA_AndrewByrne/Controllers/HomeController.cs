using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClockSystemCA_AndrewByrne.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var model = _userService.GetUserById(1);

            //_userService.AddUser(new DAL.Entities.User
            //{
            //    FirstName = "NewTest",
            //    SecondName = "NewTest2",
            //    Email = "T@T.com",
            //    RoleId = 2,
            //    Password = "Test"
            //});

            //var users = _userService.GetUsers();

            //while (users.Count > 1)
            //{
            //    _userService.DeleteUser(users.Last().UserId);

            //    users = _userService.GetUsers();
            //}

            //users = _userService.GetUsers();

            return View();
        }

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
