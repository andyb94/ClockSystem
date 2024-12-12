using ClockSystem.Services.Models;
using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IClockRecordService
    {
        public List<ClockRecord> GetRecords(int userId);
        public Task<bool> AddRecord(int userId, int clockType);
        public Task<List<ClockRecord>> GetRecords(DateTime clockDate);
        public Task<List<ClockRecord>> GetRecords(int userId, DateTime clockDate);
        public Task<WeeklyClockRecords> GetWeeklyRecords(int userId, DateTime date);
        public void DeleteRecord(int userId, DateTime clockTime);
        public void UpdateRecord(int userId, DateTime clockTime, DateTime newClockTime);
        public int GetMaxClockPerDay(WeeklyClockRecords weeklyClockRecords);
    }
}
