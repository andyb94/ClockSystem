using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class AbsenceRecordRepo : IAbsenceRecordRepo
    {
        // Using direct injection for DB context to repos. setting it at creation.
        private readonly AppDbContext ctx;
        public AbsenceRecordRepo(AppDbContext context) { 
            ctx = context;
        }

        public AbsenceRecord GetAbsenceRecordById(int id)
        {
            var absenceRecord = ctx.AbsenceRecords.FirstOrDefault(x => x.AbsenceRecordId == id);

            return absenceRecord;
        }

        public void AddAbsenceRecord(AbsenceRecord absenceRecord)
        {
            var exist = ctx.AbsenceRecords.FirstOrDefault(x => x.AbsenceRecordId == absenceRecord.AbsenceRecordId);

            if (exist == null)
            {
                ctx.AbsenceRecords.Add(absenceRecord);

                ctx.SaveChanges();
            }
        }

        public List<AbsenceRecord> GetAbsenceRecords()
        {
            var absenceRecords = ctx.AbsenceRecords.ToList();

            return absenceRecords;
        }

        public async Task<List<AbsenceRecord>> GetAbsenceRecordsForDate(DateTime date)
        {
            var absenceRecords = await ctx.AbsenceRecords.Where(x => x.AbsenceDate.Date == date.Date).ToListAsync();

            return absenceRecords;
        }

        public async Task<List<AbsenceRecord>> GetAbsenceRecordsByTypeAndDate(int absenceType, DateTime date)
        {
            var absenceRecords = await ctx.AbsenceRecords.Join(ctx.AbsenceRequests, 
                                abrec => abrec.AbsenceRequestId, abreq => abreq.AbsenceRequestId,
                                (abrec, abreq) => new { abrec, abreq }
                                ).Where(x => x.abrec.AbsenceDate == date.Date 
                                && x.abreq.AbsenceTypeId == absenceType)
                                .Select(x => x.abrec).ToListAsync();

            return absenceRecords;
        }

        public void DeleteAbsenceRecord(int id)
        {
            var exist = ctx.AbsenceRecords.FirstOrDefault(x => x.AbsenceRecordId == id);

            if (exist != null)
            {
                ctx.AbsenceRecords.Remove(exist);

                ctx.SaveChanges();
            }
        }

        public void UpdateAbsenceRecord(AbsenceRecord absenceRecord)
        {
            var exist = ctx.AbsenceRecords.FirstOrDefault(x => x.AbsenceRecordId == absenceRecord.AbsenceRecordId);

            if (exist != null)
            {
                //exist.RequestName = AbsenceRequest.RequestName;

                ctx.SaveChanges();
            }
        }
    }
}
