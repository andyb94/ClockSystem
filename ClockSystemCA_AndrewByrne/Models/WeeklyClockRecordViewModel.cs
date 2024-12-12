using ClockSystem.Services.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Models
{
    public class WeeklyClockRecordViewModel
    {
        public WeeklyClockRecords Records { get; set; }
        public User User { get; set; }
        public SelectList ListOfClockTypes { get; set; } 
        public int MaxClockPerDay { get; set; }
        public double FlexiRecord { get; set; }
    }
}
