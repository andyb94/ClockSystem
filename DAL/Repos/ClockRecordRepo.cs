
using System.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class ClockRecordRepo : IClockRecordRepo
    {
        // Using direct injection for DB context to repos. setting it at creation.
        private readonly AppDbContext ctx;
        public ClockRecordRepo(AppDbContext context) { 
            ctx = context;
        }

        public List<ClockRecord> GetRecords(int userId)
        {
            var records = ctx.ClockRecords.Where(x => x.UserId == userId).ToList();

            return records;
        }

        public async Task<bool> AddRecordAsync(int userId, int clockType)
        {
            var date = DateTime.UtcNow.Date;
            var records = ctx.ClockRecords.Where(x => x.UserId == userId 
                                                    && x.ClockTime.Date == date)
                                            .OrderBy(x => x.DayClockOrder);

            var model = new ClockRecord
            {
                ClockTypeId = clockType,
                UserId = userId,
                ClockTime = DateTime.UtcNow
            };

            if (records != null && records.Count() > 0)
            {
                var record = records.Last();

                // Clocked Status is always opposite of last record, order is always one greater
                model.ClockedIn = !record.ClockedIn;
                model.DayClockOrder = record.DayClockOrder + 1;
            }
            else
            {
                // If no records yet give default for first of day
                model.ClockedIn = true;
                model.DayClockOrder = 1;
            }

            ctx.ClockRecords.Add(model);

            await ctx.SaveChangesAsync();

            return true;
        }

        public async Task<List<ClockRecord>> GetRecords(DateTime clockDate)
        {
            var records = await ctx.ClockRecords.Where(x => x.ClockTime.Date == clockDate.Date).ToListAsync();

            return records;
        }

        public async Task<List<ClockRecord>> GetRecords(DateTime clockDate, int clockType)
        {
            var records = await ctx.ClockRecords.Where(x => x.ClockTime.Date == clockDate.Date
                                    && x.ClockTypeId == clockType).ToListAsync();

            return records;
        }

        public async Task<List<ClockRecord>> GetRecords(int userId, DateTime clockDate)
        {
            var records = await ctx.ClockRecords.Where(x => x.ClockTime.Date == clockDate.Date
                                                && x.UserId == userId).OrderBy(x => x.DayClockOrder).ToListAsync();

            return records;
        }

        public List<ClockRecord> GetRecords(int userId, DateTime startDate, DateTime endDate)
        {
            var records = ctx.ClockRecords.Where(x => x.ClockTime.Date >= startDate.Date
                                                && x.ClockTime.Date <= endDate.Date
                                                && x.UserId == userId).ToList();

            return records;
        }

        public void DeleteRecord(int userId, DateTime clockTime)
        {
            var exist = ctx.ClockRecords.FirstOrDefault(x => x.UserId == userId && x.ClockTime == clockTime);

            if (exist != null)
            {
                ctx.ClockRecords.Remove(exist);

                ctx.SaveChanges();
            }
        }

        public void UpdateRecord(int userId, DateTime clockTime, DateTime newClockTime)
        {
            var exist = ctx.ClockRecords.FirstOrDefault(x => x.UserId == userId && x.ClockTime == clockTime);

            if (exist != null)
            {
                exist.ClockTime = newClockTime;

                ctx.SaveChanges();
            }
        }
    }
}
