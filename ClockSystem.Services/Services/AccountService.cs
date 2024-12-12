using ClockSystem.Services.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClockSystem.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepo _userRepo;
        private readonly PasswordHasher<User> _passwordHasher;
        
        public AccountService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _passwordHasher = new PasswordHasher<User>();
        }

        public User ValidateUser(string email, string password)
        {
            // First get the user, using email entered for login
            var user = _userRepo.GetUserByEmail(email);
            // if user is not null begin confirming password
            if (user != null && user.Password != null) {
                // passwordHasher compares the password from user to entered password
                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
                // Confirm the result was a success. passwordHasher method returns type below
                if (result == PasswordVerificationResult.Success)
                {
                    return user;
                }
            }
            // Allow null if fail
            return null;
        }

    }
}
