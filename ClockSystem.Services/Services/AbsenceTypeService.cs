using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;

namespace ClockSystem.Services.Services
{
    public class AbsenceTypeService : IAbsenceTypeService
    {
        private readonly IAbsenceTypeRepo _absenceTypeRepo;
        public AbsenceTypeService(IAbsenceTypeRepo absenceTypeRepo)
        {
            _absenceTypeRepo = absenceTypeRepo;
        }

        public AbsenceType GetAbsenceTypeById(int id)
        {
            return _absenceTypeRepo.GetAbsenceTypeById(id);
        }

        public void AddAbsenceType(AbsenceType absenceType) {
            _absenceTypeRepo.AddAbsenceType(absenceType);
        }

        public List<AbsenceType> GetAbsenceTypes() { 
            return _absenceTypeRepo.GetAbsenceTypes();
        }

        public void DeleteAbsenceType(int id)
        {
            _absenceTypeRepo.DeleteAbsenceType(id);
        }

        public void UpdateAbsenceType(AbsenceType absenceType)
        {
            _absenceTypeRepo.UpdateAbsenceType(absenceType);
        }
    }
}
