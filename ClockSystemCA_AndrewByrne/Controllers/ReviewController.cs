using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Controllers
{
    // Only Admins can approve/reject
    [Authorize (Roles = "Admin")]
    public class ReviewController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IAbsenceTypeService _absenceTypeService;
        private readonly IAbsenceRequestService _absenceRequestService;
        private readonly IRoleService _roleService;
        private readonly UserHelper _userHelper;

        public ReviewController(ILogger<HomeController> logger, 
            IUserService userService, UserHelper userHelper,
            IAbsenceTypeService absenceTypeService, IAbsenceRequestService absenceRequestService,
            IRoleService roleService)
        {
            _logger = logger;
            _userService = userService;
            _userHelper = userHelper;
            _absenceTypeService = absenceTypeService;
            _absenceRequestService = absenceRequestService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            // Get user from cookie
            var userId = _userHelper.GetCurrentUserId();
            // get list of absence types as keyvaluepair list, GetAbsenceTypes returns list and convert using linq
            var absenceTypes = _absenceTypeService.GetAbsenceTypes()
                .Select(x => new KeyValuePair<int, string>(x.AbsenceTypeId, x.AbsenceTypeName))
                .OrderBy(x => x.Key);

            // get list of roles as keyvaluepair list, GetRoles returns list and convert using linq
            var roles = _roleService.GetRoles()
                .Select(x => new KeyValuePair<int, string>(x.RoleId, x.RoleName))
                .OrderBy(x => x.Key);

            AbsenceRequestsViewModel model = new AbsenceRequestsViewModel
            {
                UserId = userId,
                ListOfAbsenceTypes = new SelectList(absenceTypes, "Key", "Value", 0),
                ListRoleTypes = new SelectList(roles, "Key", "Value", 0)
            };

            return View(model);
        }

        // For Approval and Rejections
        [HttpPost]
        public async Task<IActionResult> CompleteRequest(int requestId, int userId, 
                                                        bool approved)
        {
            _absenceRequestService.UpdateAbsenceRequest(requestId, userId, approved);

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> RequestsForReviewPartial(int userId, int roleTypeId, int absenceTypeId, 
                                                                DateTime startDate, DateTime endDate)
        {

            var requests = await _absenceRequestService.GetAbsenceRequestsForAdminReview(userId, roleTypeId,
                                                                absenceTypeId, startDate, endDate);

            AbsenceRequestsViewModel model = new AbsenceRequestsViewModel
            {
                ListOfAbsenceRequests = requests
            };

            return PartialView(model);
        }
    }
}
