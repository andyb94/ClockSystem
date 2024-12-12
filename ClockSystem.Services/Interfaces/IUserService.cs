using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IUserService
    {
        public User GetUserById(int id);
        public void AddUser(User user);
        public Task<List<User>> GetUsers();
        public bool DeleteUser(int id);
        public void UpdateUser(User user);
        public void UpdatePassword(User user);
        public User GetUserByEmail(string email);
    }
}
