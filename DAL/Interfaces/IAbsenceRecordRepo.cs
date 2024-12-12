
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IAbsenceRecordRepo
    {
        public AbsenceRecord GetAbsenceRecordById(int id);
        public void AddAbsenceRecord(AbsenceRecord absenceRecord);
        public List<AbsenceRecord> GetAbsenceRecords();
        public Task<List<AbsenceRecord>> GetAbsenceRecordsForDate(DateTime date);
        public Task<List<AbsenceRecord>> GetAbsenceRecordsByTypeAndDate(int absenceType, DateTime date);
        public void DeleteAbsenceRecord(int id);
        public void UpdateAbsenceRecord(AbsenceRecord absenceRecord);
    }
}
