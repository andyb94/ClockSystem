
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class RoleRepo : IRoleRepo
    {
        // Using direct injection for DB context to repos. setting it at creation.
        private readonly UserDbContext ctx;
        public RoleRepo(UserDbContext context) { 
            ctx = context;
        }

        // Get Role by RoleId. The id passed in is to match RoleId
        public Role GetRoleById(int id)
        {            
            Role role = ctx.Roles.FirstOrDefault(x => x.RoleId == id);

            return role;
        }

        public void AddRole(Role role)
        {
            ctx.Roles.Add(role);
            ctx.SaveChanges();
        }

        public List<Role> GetRoles()
        { 
            List<Role> roles = ctx.Roles.ToList();

            return roles;
        }

        public void DeleteRole(int id)
        {
            var role = ctx.Roles.FirstOrDefault(x => x.RoleId == id);
            if (role != null) { 
                ctx.Roles.Remove(role);
                ctx.SaveChanges();
            }
        }

        public void UpdateRole(Role role) { 
            var exist = ctx.Roles.FirstOrDefault(x => x.RoleId == role.RoleId);

            if (exist != null)
            {
                exist.RoleName = role.RoleName;

                ctx.SaveChanges();
            }
        }
    }
}
