using DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClockSystemCA_AndrewByrne.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public SelectList ListOfRoles { get; set; } 
    }
}
