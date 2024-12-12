
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IClockRecordRepo
    {
        public List<ClockRecord> GetRecords(int userId);
        public Task<bool> AddRecordAsync(int userId, int clockType);
        public Task<List<ClockRecord>> GetRecords(DateTime clockDate);
        public Task<List<ClockRecord>> GetRecords(DateTime clockDate, int clockType);
        public Task<List<ClockRecord>> GetRecords(int userId, DateTime clockDate);
        public List<ClockRecord> GetRecords(int userId, DateTime startDate, DateTime endDate);
        public void DeleteRecord(int userId, DateTime clockTime);
        public void UpdateRecord(int userId, DateTime clockTime, DateTime newClockTime);
    }
}
