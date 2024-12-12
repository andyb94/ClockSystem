using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Controllers
{
    [Authorize]
    public class AbsenceController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IAbsenceTypeService _absenceTypeService;
        private readonly IAbsenceRequestService _absenceRequestService;
        private readonly UserHelper _userHelper;

        public AbsenceController(ILogger<HomeController> logger, 
            IUserService userService, UserHelper userHelper,
            IAbsenceTypeService absenceTypeService, IAbsenceRequestService absenceRequestService)
        {
            _logger = logger;
            _userService = userService;
            _userHelper = userHelper;
            _absenceTypeService = absenceTypeService;
            _absenceRequestService = absenceRequestService;
        }

        public async Task<IActionResult> Index()
        {
            // Get user from cookie
            var userId = _userHelper.GetCurrentUserId();
            // get list of absence types as keyvaluepair list, GetAbsenceTypes returns list and convert using linq
            var absenceTypes = _absenceTypeService.GetAbsenceTypes()
                .Select(x => new KeyValuePair<int, string>(x.AbsenceTypeId, x.AbsenceTypeName))
                .OrderBy(x => x.Key);

            var absenceRequests = await _absenceRequestService.GetActiveAbsenceRequestsByUser(userId);

            UserAbsenceViewModel model = new UserAbsenceViewModel
            {
                UserId = userId,
                ListOfAbsenceTypes = new SelectList(absenceTypes, "Key", "Value", 0),
                ListOfAbsenceRequests = absenceRequests
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequest(int userId, int absenceTypeId, DateTime startDate,
                                                DateTime endDate)
        {
            // AbsenceTypeId 3 is flexi, can only take one day at a time with this so start date must match end date
            if (absenceTypeId == 3 && startDate.Date != endDate.Date)
            {
                return Json(false);
            }

            // First confirm no Request exists with overlapping dates
            var exist = _absenceRequestService.CheckIfRequestExistForDates(userId, startDate, endDate);

            // if exists return as false, can't insert when dates overlap
            if (exist)
            {
                return Json(false);
            }

            // Doesn't exist so insert
            var res = _absenceRequestService.AddAbsenceRequest(userId, absenceTypeId, startDate, endDate);

            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> CancelRequest(int requestId)
        {
            // Delete the request, user doesn't want anymore
            _absenceRequestService.DeleteAbsenceRequest(requestId);

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> PersonalRequestsPartial(int userId)
        {
            // Returning list of active requests for user. Not approved/rejected yet
            UserAbsenceViewModel model = new UserAbsenceViewModel
            {
                UserId = userId
            };
            model.ListOfAbsenceRequests = await _absenceRequestService.GetActiveAbsenceRequestsByUser(userId);

            return PartialView(model);
        }
    }
}
