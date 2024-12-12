using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClockSystem.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IFlexiService _flexiService;
        private readonly PasswordHasher<User> _passwordHasher;
        public UserService(IUserRepo userRepo, IFlexiService flexiService)
        {
            _userRepo = userRepo;
            _flexiService = flexiService;
            _passwordHasher = new PasswordHasher<User>();
        }

        public User GetUserById(int id)
        {
            return _userRepo.GetUserById(id);
        }

        public void AddUser(User user) {            
            user = HashPassword(user);
            _userRepo.AddUser(user);

            // Once user is created create a flexi record to be used during clocking process
            // default amount of 0 hours
            // User did not have an id in object as it was not yet created so get it by email now
            var newUser = _userRepo.GetUserByEmail(user.Email);
            _flexiService.AddFlexiRecord(newUser.UserId, 0f);
        }

        public async Task<List<User>> GetUsers() { 
            return await _userRepo.GetUsers();
        }

        public bool DeleteUser(int id)
        {
            return _userRepo.DeleteUser(id);
        }

        // Personal data only, not password
        public void UpdateUser(User user)
        {
            _userRepo.UpdateUser(user);
        }

        // only password update
        public void UpdatePassword(User user)
        {
            // Hash password before
            user = HashPassword(user);

            _userRepo.UpdateUser(user);
        }

        public User GetUserByEmail(string email) {
            var user = _userRepo.GetUserByEmail(email);

            return user;
        }

        // Hash the password for user
        private User HashPassword(User user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            return user;
        }
    }
}
