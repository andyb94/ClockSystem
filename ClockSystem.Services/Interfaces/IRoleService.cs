using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IRoleService
    {
        public Role GetRoleById(int id);
        public void AddRole(Role role);
        public List<Role> GetRoles();
        public void DeleteRole(int id);
        public void UpdateRole(Role role);
    }
}
