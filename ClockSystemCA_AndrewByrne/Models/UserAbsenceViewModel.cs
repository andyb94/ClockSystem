using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Models
{
    public class UserAbsenceViewModel
    {
        public int UserId { get; set; }
        public SelectList ListOfAbsenceTypes { get; set; } 
        public List<AbsenceRequest>? ListOfAbsenceRequests { get; set; } 
    }
}
