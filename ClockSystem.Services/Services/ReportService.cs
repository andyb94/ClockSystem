using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;
using ClockSystem.Services.Models;

namespace ClockSystem.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IClockRecordRepo _clockRecordRepo;
        private readonly IAbsenceRecordRepo _absenceRecordRepo;
        private readonly IUserRepo _userRepo;
        public ReportService(IClockRecordRepo clockRecordRepo, IUserRepo userRepo, IAbsenceRecordRepo absenceRecordRepo)
        {
            _clockRecordRepo = clockRecordRepo;
            _userRepo = userRepo;
            _absenceRecordRepo = absenceRecordRepo;
        }

        // Get record of days missed completely or paritally clocked
        // Ignore weekends as they are only for overtime
        public async Task<IncompleteWorkedDaysRecords> GetIncompleteWorkDays(DateTime startDate,
                                                                            DateTime endDate)
        {

            IncompleteWorkedDaysRecords incompleteWorkedDays = new IncompleteWorkedDaysRecords();
            DateTime date = startDate;

            // Need users for getting missed days and incomplete days correctly
            var users = await _userRepo.GetUsers();
            while (date.Date <= endDate.Date)
            {
                // records for the day
                List<ClockRecord> records = await _clockRecordRepo.GetRecords(date);
                // day used for switch
                var dayName = date.DayOfWeek;

                // ints to use for counting
                int missedDays = 0;
                int incompleteDays = 0;

                // Missed days is the user count minus the users with records. no records for a user
                // means a missed day
                var recordUsers = records.Select(x => x.UserId).Distinct().ToList();
                var absences = await _absenceRecordRepo.GetAbsenceRecordsForDate(date);
                missedDays = users.Count() - recordUsers.Count();

                // if absences exist then minus that from missed days count as they don't count
                if (absences != null && absences.Count > 0)
                {
                    missedDays -= absences.Count();
                }

                // check records per user if days are missed
                foreach (var recordUser in recordUsers)
                {
                    // Ternery used. If Day is partial for user add 1 if not add 0 (stay same)
                    incompleteDays += IsPartialDay(records.Where(x => x.UserId == recordUser).ToList()) ? 1 : 0;
                }

                switch (dayName)
                {
                    case DayOfWeek.Monday:
                        incompleteWorkedDays.MonMissedDays += missedDays;
                        incompleteWorkedDays.MonIncompleteDays += incompleteDays;
                        break;
                    case DayOfWeek.Tuesday:
                        incompleteWorkedDays.TueMissedDays += missedDays;
                        incompleteWorkedDays.TueIncompleteDays += incompleteDays;
                        break;
                    case DayOfWeek.Wednesday:
                        incompleteWorkedDays.WedMissedDays += missedDays;
                        incompleteWorkedDays.WedIncompleteDays += incompleteDays;
                        break;
                    case DayOfWeek.Thursday:
                        incompleteWorkedDays.ThurMissedDays += missedDays;
                        incompleteWorkedDays.ThurIncompleteDays += incompleteDays;
                        break;
                    case DayOfWeek.Friday:
                        incompleteWorkedDays.FriMissedDays += missedDays;
                        incompleteWorkedDays.FriIncompleteDays += incompleteDays;
                        break;
                    default:
                        break;
                }

                // increment the day, AddDays returns a new DateTime so need to assign to date again
                date = date.AddDays(1);
            }

            incompleteWorkedDays.TotalMissedDays = incompleteWorkedDays.MonMissedDays + incompleteWorkedDays.TueMissedDays
                                    + incompleteWorkedDays.WedMissedDays + incompleteWorkedDays.ThurMissedDays
                                    + incompleteWorkedDays.FriMissedDays;

            incompleteWorkedDays.TotalIncompleteDays = incompleteWorkedDays.MonIncompleteDays + incompleteWorkedDays.TueIncompleteDays
                                    + incompleteWorkedDays.WedIncompleteDays + incompleteWorkedDays.ThurIncompleteDays
                                    + incompleteWorkedDays.FriIncompleteDays;

            return incompleteWorkedDays;
        }

        // Get record of days missed completely or paritally clocked
        // Ignore weekends as they are only for overtime
        public async Task<AbsentDayRecords> GetAbsentDays(DateTime startDate,
                                                                            DateTime endDate)
        {

            AbsentDayRecords absentDays = new AbsentDayRecords();
            DateTime date = startDate;

            while (date.Date <= endDate.Date)
            {
                // day used for switch
                var dayName = date.DayOfWeek;

                // ints to use for counting
                int holidaysDays = 0;
                int sickDays = 0;

                // absences for the days
                var sickDayList = await _absenceRecordRepo.GetAbsenceRecordsByTypeAndDate(1, date);
                var holidayList = await _absenceRecordRepo.GetAbsenceRecordsByTypeAndDate(2, date);

                // check if lists not empty and null then add to counts
                sickDays += sickDayList != null && sickDayList.Count > 0 ? sickDayList.Count : 0;
                holidaysDays += holidayList != null && holidayList.Count > 0 ? holidayList.Count : 0;

                switch (dayName)
                {
                    case DayOfWeek.Monday:
                        absentDays.MonSickDays += sickDays;
                        absentDays.MonHolidayDays += holidaysDays;
                        break;
                    case DayOfWeek.Tuesday:
                        absentDays.TueSickDays += sickDays;
                        absentDays.TueHolidayDays += holidaysDays;
                        break;
                    case DayOfWeek.Wednesday:
                        absentDays.WedSickDays += sickDays;
                        absentDays.WedHolidayDays += holidaysDays;
                        break;
                    case DayOfWeek.Thursday:
                        absentDays.ThurSickDays += sickDays;
                        absentDays.ThurHolidayDays += holidaysDays;
                        break;
                    case DayOfWeek.Friday:
                        absentDays.FriSickDays += sickDays;
                        absentDays.FriHolidayDays += holidaysDays;
                        break;
                    default:
                        break;
                }

                absentDays.TotalSickDays += sickDays;
                absentDays.TotalHolidayDays += holidaysDays;

                // increment the day, AddDays returns a new DateTime so need to assign to date again
                date = date.AddDays(1);
            }

            return absentDays;
        }


        // Get record of days missed completely or paritally clocked
        // Ignore weekends as they are only for overtime
        public async Task<OvertimeRecords> GetOvertimeDays(DateTime startDate,
                                                                            DateTime endDate)
        {

            OvertimeRecords overtime = new OvertimeRecords();
            DateTime date = startDate;

            while (date.Date <= endDate.Date)
            {
                // day used for switch
                var dayName = date.DayOfWeek;

                // ints to use for counting
                int overtimeDays = 0;

                // overtime days. Overtime is 3 in clockTypes DB
                var overtimeList = await _clockRecordRepo.GetRecords(date, 3);

                // check if lists not empty and null then add to counts
                overtimeDays += overtimeList != null && overtimeList.Count > 0 ? overtimeList.Count : 0;
                // Overtime is only Sat/Sun
                switch (dayName)
                {
                    case DayOfWeek.Saturday:
                        overtime.SatOvertimeDays += overtimeDays;
                        break;
                    case DayOfWeek.Sunday:
                        overtime.SunOvertimeDays += overtimeDays;
                        break;
                    default:
                        break;
                }

                overtime.TotalOvertimeDays += overtimeDays;

                // increment the day, AddDays returns a new DateTime so need to assign to date again
                date = date.AddDays(1);
            }

            return overtime;
        }



        // If hours worked is less than 7 return true
        private bool IsPartialDay(List<ClockRecord> records)
        {
            double hours = 0;
            if (records.Count>1)
            {
                // Index increase by two ever loop as cal uses two items per time
                // First is in second is out so increase by two keeps the pattern
                int index = 0;
                for (int i = 0; i < records.Count() / 2; i++)
                {
                    hours = (records[index + 1].ClockTime.Subtract(records[index].ClockTime)).TotalHours;
                    index += 2;
                }
            }
            // 7 is considered full day
            return hours < 7;
        }

    }
}
