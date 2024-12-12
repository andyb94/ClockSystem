
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class UserRepo : IUserRepo
    {
        // Using direct injection for DB context to repos. setting it at creation.
        private readonly UserDbContext ctx;
        public UserRepo(UserDbContext context) { 
            ctx = context;
        }

        // Get user by userId. The id passed in is to match userId
        public User GetUserById(int id)
        {            
            // UserRole is an internal Entity of Role inside User entity. Include brings that in as well
            User user = ctx.Users.Include(x => x.UserRole).FirstOrDefault(x => x.UserId == id);

            return user;
        }

        // Get user by email. The email passed in is to match email in entity
        public User GetUserByEmail(string email)
        {
            // UserRole is an internal Entity of Role inside User entity. Include brings that in as well
            User user = ctx.Users.Include(x => x.UserRole).FirstOrDefault(x => x.Email == email);

            return user;
        }

        public void AddUser(User user)
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        // Return a task so calling method can await
        public async Task<List<User>> GetUsers()
        {
            // UserRole is an internal Entity of Role inside User entity. Include brings that in as well
            List<User> users = await ctx.Users.Where(x => !x.IsDeleted).Include(x => x.UserRole).ToListAsync();

            // return users as a task
            return users;
        }

        public async Task<List<User>> GetUsersByRole(int roleType)
        {
            // UserRole is an internal Entity of Role inside User entity. Include brings that in as well
            List<User> users = await ctx.Users.Where(x => x.RoleId == roleType && !x.IsDeleted).ToListAsync();

            // return users as a task
            return users;
        }

        public bool DeleteUser(int id)
        {
            var user = ctx.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null) {
                user.IsDeleted = true;
                ctx.Users.Update(user);
                ctx.SaveChanges();

                return true;
            }

            return false;
        }

        public void UpdateUser(User user) { 
            var exist = ctx.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (exist != null)
            {
                exist.FirstName = user.FirstName;
                exist.SecondName = user.SecondName;
                exist.Email = user.Email;
                exist.RoleId = user.RoleId;

                ctx.SaveChanges();
            }
        }

        public void UpdatePassword(User user)
        {
            var exist = ctx.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (exist != null)
            {
                exist.Password = user.Password;

                ctx.SaveChanges();
            }
        }
    }
}
