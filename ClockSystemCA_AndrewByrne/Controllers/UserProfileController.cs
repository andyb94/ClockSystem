using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Interfaces to be used
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly UserHelper _userHelper;

        // Pass Interfaces into constructor, dependency injection
        public UserProfileController(ILogger<HomeController> logger, 
            IUserService userService, IRoleService roleService, UserHelper userHelper)
        {
            // Set passed in interfaces to the local ref for class.
            _logger = logger;
            _userService = userService;
            _roleService = roleService;
            _userHelper = userHelper;
        }

        // Index/Home page of User Manager
        public IActionResult Index()
        {
            UserViewModel model = new UserViewModel();
            var userId = _userHelper.GetCurrentUserId();
            // populate model with all users
            model.User = _userService.GetUserById(userId);
            return View(model);
        }

        // Edit User View
        public IActionResult EditUser(int userId)
        {
            UserViewModel model = new UserViewModel();
            // populate model with the current user
            model.User = _userService.GetUserById(userId);
            // get list of roles as keyvaluepair list, GetRoles returns list and convert using linq
            var roles = _roleService.GetRoles()
                .Select(x => new KeyValuePair<int, string>(x.RoleId, x.RoleName))
                .OrderBy(x => x.Key);

            // populate model ListOfRoles with a select list created from roles
            model.ListOfRoles = new SelectList(roles, "Key", "Value", model.User.RoleId);
            return View(model);
        }


        // TODO Refactor out to API Controller
        // Post Methods
        // Update user with details from form posted        
        [HttpPost]
        public JsonResult EditUser(int userId, string firstName, string secondName)
        {
            // Create view model and get the current User model assigned to the user
            UserViewModel model = new UserViewModel();
            model.User = _userService.GetUserById(userId);

            // Update User inside model with passed details
            model.User.FirstName = firstName;
            model.User.SecondName = secondName;

            // Send updated User to be updated
            _userService.UpdateUser(model.User);

            // Get the updated user from DB to confirm the update
            model.User = _userService.GetUserById(model.User.UserId);

            // Return the result to the post in view
            return Json(true);
        }

        // Update user with details from form posted        
        [HttpPost]
        public JsonResult UpdatePassword(int userId, string password)
        {
            // Create view model and get the current User model assigned to the user
            UserViewModel model = new UserViewModel();
            model.User = _userService.GetUserById(userId);

            // Update User inside model with passed details
            model.User.Password = password;

            // Send updated User to be updated
            _userService.UpdatePassword(model.User);

            // Return the result to the post in view
            return Json(true);
        }

    }
}
