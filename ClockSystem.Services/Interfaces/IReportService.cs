using ClockSystem.Services.Models;

namespace ClockSystem.Services.Interfaces
{
    public interface IReportService
    {
        public Task<IncompleteWorkedDaysRecords> GetIncompleteWorkDays(DateTime date, DateTime endDate);
        public Task<AbsentDayRecords> GetAbsentDays(DateTime date, DateTime endDate);
        public Task<OvertimeRecords> GetOvertimeDays(DateTime startDate, DateTime endDate);
    }
}
