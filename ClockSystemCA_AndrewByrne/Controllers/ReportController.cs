using ClockSystem.Services.Interfaces;
using ClockSystemCA_AndrewByrne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClockSystemCA_AndrewByrne.Controllers
{
    // Reports are only for Admins
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Interfaces to be used
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IReportService _reportService;

        // Pass Interfaces into constructor, dependency injection
        public ReportController(ILogger<HomeController> logger, 
            IUserService userService, IRoleService roleService, IReportService reportService)
        {
            // Set passed in interfaces to the local ref for class.
            _logger = logger;
            _userService = userService;
            _roleService = roleService;
            _reportService = reportService;
        }

        // Index/Home page of Report Controller
        public async Task<IActionResult> Index(bool overtimeReport)
        {
            ReportViewModel model = new ReportViewModel();

            // Page will filter which form shows based on bool 
            model.OvertimeReport = overtimeReport;

            return View(model);
        }

        // Partial View called from Ajax to allow report update without reload
        [HttpGet]
        public async Task<IActionResult> AdminReport(DateTime startDate, DateTime endDate, bool includeAbsent)
        {
            ReportViewModel model = new ReportViewModel();

            model.PartialAndMissedDays = await _reportService.GetIncompleteWorkDays(startDate, endDate);
            if (includeAbsent)
            {
                model.AbsentDays = await _reportService.GetAbsentDays(startDate, endDate);
            }

            model.IncludeAbsent = includeAbsent;

            return PartialView(model);
        }

        // Partial View called from Ajax to allow report update without reload
        [HttpGet]
        public async Task<IActionResult> OvertimeReport(DateTime startDate, DateTime endDate)
        {
            ReportViewModel model = new ReportViewModel();

            model.OvertimeDays = await _reportService.GetOvertimeDays(startDate, endDate);
            
            return PartialView(model);
        }

    }
}
