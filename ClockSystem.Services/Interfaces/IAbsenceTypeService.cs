using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IAbsenceTypeService
    {
        public AbsenceType GetAbsenceTypeById(int id);
        public void AddAbsenceType(AbsenceType absenceType);
        public List<AbsenceType> GetAbsenceTypes();
        public void DeleteAbsenceType(int id);
        public void UpdateAbsenceType(AbsenceType absenceType);
    }
}
