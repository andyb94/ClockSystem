using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IAbsenceRecordService
    {
        public AbsenceRecord GetAbsenceRecordById(int id);
        public void AddAbsenceRecords(AbsenceRequest absenceRequest);
        public List<AbsenceRecord> GetAbsenceRecords();
        public void DeleteAbsenceRecord(int id);
        public void UpdateAbsenceRecord(AbsenceRecord absenceRecord);
    }
}
