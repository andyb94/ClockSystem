using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Models
{
    public class AddClockRecordViewModel
    {
        public int UserId { get; set; }
        public SelectList ListOfClockTypes { get; set; } 
    }
}
