using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;
using ClockSystem.Services.Models;
using ClockSystemCA_AndrewByrne.Extensions;

namespace ClockSystem.Services.Services
{
    public class ClockRecordService : IClockRecordService
    {
        private readonly IClockRecordRepo _clockRecordRepo;
        private readonly IFlexiService _flexiService;
        public ClockRecordService(IClockRecordRepo clockRecordRepo, IFlexiService flexiService)
        {
            _clockRecordRepo = clockRecordRepo;
            _flexiService = flexiService;
        }

        public List<ClockRecord> GetRecords(int userId)
        {
            return _clockRecordRepo.GetRecords(userId);
        }

        public async Task<bool> AddRecord(int userId, int clockType) {
            bool result = await _clockRecordRepo.AddRecordAsync(userId, clockType);

            // Should update flexi as well
            if (result)
            {
                // Get the latest records from today and calculate if there is more flexi to add
                var records = await GetRecords(userId, DateTime.Now);

                // Records can't be null and must be greater than 1 and the modular res is 0 meaing it is even
                // even value means the last clock was an out clock so flexi can be updated
                if (records != null && records.Count > 1 && records.Count % 2 == 0)
                {
                    var index = records.Count - 1;
                    // flexi needs to be float so casting
                    float flexi = (float)(records[index].ClockTime.Subtract(records[index - 1].ClockTime)).TotalHours;

                    _flexiService.UpdateFlexiRecord(userId, flexi);
                }
            }

            return result;
        }

        public async Task<List<ClockRecord>> GetRecords(DateTime clockDate)
        { 
            return await _clockRecordRepo.GetRecords(clockDate);
        }

        public async Task<List<ClockRecord>> GetRecords(int userId, DateTime clockDate)
        {
            return await _clockRecordRepo.GetRecords(userId, clockDate);
        }

        public void DeleteRecord(int userId, DateTime clockTime)
        {
            _clockRecordRepo.DeleteRecord(userId, clockTime);
        }

        public void UpdateRecord(int userId, DateTime clockTime, DateTime newClockTime)
        {
            _clockRecordRepo.UpdateRecord(userId, clockTime, newClockTime);
        }

        public async Task<WeeklyClockRecords> GetWeeklyRecords(int userId, DateTime date)
        {
            // Make sure date is the start of week
            date = Helper.GetStartOfWeek(date);

            WeeklyClockRecords weeklyClockRecords = new WeeklyClockRecords();

            // Add records for full week
            for (int i = 0; i < 7; i++)
            {
                // OrderBy is quick sort
                List<ClockRecord> records = await GetRecords(userId, date);
                records = records.OrderBy(x => x.DayClockOrder).ToList();
                switch (i)
                {
                    case 0:
                        weeklyClockRecords.MonRecords = records;
                        break;
                    case 1:
                        weeklyClockRecords.TueRecords = records;
                        break;
                    case 2:
                        weeklyClockRecords.WedRecords = records;
                        break;
                    case 3:
                        weeklyClockRecords.ThurRecords = records;
                        break;
                    case 4:
                        weeklyClockRecords.FriRecords = records;
                        break;
                    case 5:
                        weeklyClockRecords.SatRecords = records;
                        break;
                    case 6:
                        weeklyClockRecords.SunRecords = records;
                        break;
                    default:
                        break;
                }

                date = date.AddDays(1);
            }

            return weeklyClockRecords;
        }

        public int GetMaxClockPerDay(WeeklyClockRecords weeklyClockRecords)
        {
            // The most clocks in a day
            // Ternery to check if exist and if not give 0
            int[] maxClocks = [weeklyClockRecords.MonRecords.Any() ? weeklyClockRecords.MonRecords.Max(x => x.DayClockOrder) : 0,
                                weeklyClockRecords.TueRecords.Any() ? weeklyClockRecords.TueRecords.Max(x => x.DayClockOrder) : 0,
                                weeklyClockRecords.WedRecords.Any() ? weeklyClockRecords.WedRecords.Max(x => x.DayClockOrder) : 0,
                                weeklyClockRecords.ThurRecords.Any() ? weeklyClockRecords.ThurRecords.Max(x => x.DayClockOrder) : 0,
                                weeklyClockRecords.FriRecords.Any() ? weeklyClockRecords.FriRecords.Max(x => x.DayClockOrder) : 0,
                                weeklyClockRecords.SatRecords.Any() ? weeklyClockRecords.SatRecords.Max(x => x.DayClockOrder) : 0,
                                weeklyClockRecords.SunRecords.Any() ? weeklyClockRecords.SunRecords.Max(x => x.DayClockOrder) : 0];
            return maxClocks.Max();
        }
    }
}
