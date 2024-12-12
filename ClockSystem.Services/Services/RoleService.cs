using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;

namespace ClockSystem.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepo _roleRepo;
        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public Role GetRoleById(int id)
        {
            return _roleRepo.GetRoleById(id);
        }

        public void AddRole(Role role) { 
            _roleRepo.AddRole(role);
        }

        public List<Role> GetRoles() { 
            return _roleRepo.GetRoles();
        }

        public void DeleteRole(int id)
        {
            _roleRepo.DeleteRole(id);
        }

        public void UpdateRole(Role role)
        {
            _roleRepo.UpdateRole(role);
        }
    }
}
