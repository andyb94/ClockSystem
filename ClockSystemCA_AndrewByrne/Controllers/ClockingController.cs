using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ClockSystemCA_AndrewByrne.Controllers
{
    [Authorize]
    public class ClockingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IClockTypeService _clockTypeService;
        private readonly IClockRecordService _clockRecordService;
        private readonly UserHelper _userHelper;
        private readonly IFlexiService _flexiService;

        public ClockingController(ILogger<HomeController> logger, 
            IUserService userService, IClockTypeService clockTypeService,
            IClockRecordService clockRecordService, UserHelper userHelper, IFlexiService flexiService)
        {
            _logger = logger;
            _userService = userService;
            _clockTypeService = clockTypeService;
            _clockRecordService = clockRecordService;
            _userHelper = userHelper;
            _flexiService = flexiService;
        }

        public IActionResult Index()
        {
            var userId = _userHelper.GetCurrentUserId();
            
            // get list of roles as keyvaluepair list, GetRoles returns list and convert using linq
            var clockTypes = _clockTypeService.GetClockTypes()
                .Select(x => new KeyValuePair<int, string>(x.ClockTypeId, x.TypeName))
                .OrderBy(x => x.Key);

            AddClockRecordViewModel model = new AddClockRecordViewModel
            {
                UserId = userId,
                ListOfClockTypes = new SelectList(clockTypes, "Key", "Value", 0)
            };

            return View(model);
        }

        public async Task<IActionResult> UserClockRecordsAsync() {

            WeeklyClockRecordViewModel model = new WeeklyClockRecordViewModel();

            var userId = _userHelper.GetCurrentUserId();

            model.Records = await _clockRecordService.GetWeeklyRecords(userId, DateTime.UtcNow);

            model.FlexiRecord = _flexiService.GetFlexiRecordByUserId(userId).FlexiTime;

            // The most clocks in a day
            // Ternery to check if exist and if not give 0
            model.MaxClockPerDay = _clockRecordService.GetMaxClockPerDay(model.Records);

            model.User = _userService.GetUserById(userId);

            return View(model);
        }

        // Partial View called from Ajax to allow report update without reload
        [HttpGet]
        public async Task<IActionResult> UserWeeklyReport(int userId, DateTime date)
        {

            WeeklyClockRecordViewModel model = new WeeklyClockRecordViewModel();

            model.Records = await _clockRecordService.GetWeeklyRecords(userId, date);

            model.FlexiRecord = _flexiService.GetFlexiRecordByUserId(userId).FlexiTime;

            model.MaxClockPerDay = _clockRecordService.GetMaxClockPerDay(model.Records);

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Clock(int userId, int clockTypeId)
        {
            var records = await _clockRecordService.GetRecords(userId, DateTime.UtcNow);

            await _clockRecordService.AddRecord(userId, clockTypeId);

            var updatedRecords = await _clockRecordService.GetRecords(userId, DateTime.UtcNow);

            return Json(records.Count < updatedRecords.Count);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
