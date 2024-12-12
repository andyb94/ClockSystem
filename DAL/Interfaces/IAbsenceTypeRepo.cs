
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IAbsenceTypeRepo
    {
        public AbsenceType GetAbsenceTypeById(int id);
        public void AddAbsenceType(AbsenceType absenceType);
        public List<AbsenceType> GetAbsenceTypes();
        public void DeleteAbsenceType(int id);
        public void UpdateAbsenceType(AbsenceType absenceType);
    }
}
