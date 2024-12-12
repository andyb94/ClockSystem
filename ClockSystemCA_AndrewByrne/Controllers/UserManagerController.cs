using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Controllers
{
    // Restrict this controller to Admin only users
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Interfaces to be used
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        // Pass Interfaces into constructor, dependency injection
        public UserManagerController(ILogger<HomeController> logger, 
            IUserService userService, IRoleService roleService)
        {
            // Set passed in interfaces to the local ref for class.
            _logger = logger;
            _userService = userService;
            _roleService = roleService;
        }

        // Index/Home page of User Manager
        public async Task<IActionResult> Index()
        {
            ListOfUserViewModel model = new ListOfUserViewModel();
            // populate model with all users
            model.Users = await _userService.GetUsers();
            return View(model);
        }

        // Add User View
        public IActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();
            // get list of roles as keyvaluepair list, GetRoles returns list and convert using linq
            var roles = _roleService.GetRoles()
                .Select(x => new KeyValuePair<int, string>(x.RoleId, x.RoleName))
                .OrderBy(x => x.Key);

            // populate model ListOfRoles with a select list created from roles
            model.ListOfRoles = new SelectList(roles, "Key", "Value", 0);
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

        // Add new user with details from form
        [HttpPost]
        public JsonResult AddUser(string firstName, string secondName,
                                  string email, string password, int roleId)
        {
            // Create view model and get the current User model assigned to the user
            UserViewModel model = new UserViewModel();
            var exist = _userService.GetUserByEmail(email);

            // If user exists cannot add again so return false
            if (exist != null)
            {
                return Json(false);
            }

            // Update User inside model with passed details
            model.User = new DAL.Entities.User
            {
                FirstName = firstName,
                SecondName = secondName,
                Email = email,
                Password = password,
                RoleId = roleId,
                IsDeleted = false
            };

            // Send updated User to be updated
            _userService.AddUser(model.User);

            // Get the updated user from DB to confirm the update
            exist = _userService.GetUserById(model.User.UserId);
                        
            // Return the result to the post in view, if exist is not null then it is success
            return Json(exist != null);
        }

        // Update user with details from form posted        
        [HttpPost]
        public JsonResult EditUser(int userId, int roleId)
        {
            // Create view model and get the current User model assigned to the user
            UserViewModel model = new UserViewModel();
            model.User = _userService.GetUserById(userId);

            // Update User inside model with passed details
            model.User.RoleId = roleId;

            // Send updated User to be updated
            _userService.UpdateUser(model.User);

            // Get the updated user from DB to confirm the update
            model.User = _userService.GetUserById(model.User.UserId);

            // Return the result to the post in view
            return Json(true);
        }


        // Delete user based on ID     
        [HttpPost]
        public JsonResult DeleteUser(int userId)
        {
            // delete user, exist check in repo
            var result = _userService.DeleteUser(userId);

            // Return the result to the post in view
            return Json(result);
        }
    }
}
