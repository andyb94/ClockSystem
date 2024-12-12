using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IAccountService
    {
        public User ValidateUser(string email, string password);
    }
}
