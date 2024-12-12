
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRoleRepo
    {
        public Role GetRoleById(int id);
        public void AddRole(Role role);
        public List<Role> GetRoles();
        public void DeleteRole(int id);
        public void UpdateRole(Role role);
    }
}
