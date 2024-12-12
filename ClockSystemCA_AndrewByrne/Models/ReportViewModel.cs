using ClockSystem.Services.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Models
{
    public class ReportViewModel
    {
        public IncompleteWorkedDaysRecords PartialAndMissedDays { get; set; }
        public AbsentDayRecords AbsentDays { get; set; }
        public OvertimeRecords OvertimeDays { get; set; }
        public SelectList ListOfClockTypes { get; set; }
        // List never changes so create inside model
        public SelectList IncludeAbsentSelectList { get {
                return new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Value = "true", Text = "Include" },
                    new SelectListItem { Value = "false", Text = "Exclude" }
                }, "Value", "Text");
            }
        }
        public bool IncludeAbsent { get; set; }
        public bool OvertimeReport { get; set; }
        public int MaxClockPerDay { get; set; }
    }
}
