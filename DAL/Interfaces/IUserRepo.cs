
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepo
    {
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
        public void AddUser(User user);
        public Task<List<User>> GetUsers();
        public Task<List<User>> GetUsersByRole(int roleType);
        public bool DeleteUser(int id);
        public void UpdateUser(User user);
        public void UpdatePassword(User user);
    }
}
