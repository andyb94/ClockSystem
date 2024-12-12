using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class AbsenceRequestRepo : IAbsenceRequestRepo
    {
        // Using direct injection for DB context to repos. setting it at creation.
        private readonly AppDbContext ctx;
        public AbsenceRequestRepo(AppDbContext context) { 
            ctx = context;
        }

        public AbsenceRequest GetAbsenceRequestById(int id)
        {
            var absenceRequest = ctx.AbsenceRequests.FirstOrDefault(x => x.AbsenceRequestId == id);

            return absenceRequest;
        }

        public void AddAbsenceRequest(AbsenceRequest absenceRequest)
        {
            var exist = ctx.AbsenceRequests.FirstOrDefault(x => x.AbsenceRequestId == absenceRequest.AbsenceRequestId);

            if (exist == null)
            {
                ctx.AbsenceRequests.Add(absenceRequest);

                ctx.SaveChanges();
            }
        }

        public List<AbsenceRequest> GetAbsenceRequests()
        {
            var absenceRequests = ctx.AbsenceRequests.ToList();

            return absenceRequests;
        }

        public AbsenceRequest GetAbsenceRequestByUserAndDate(int userId, DateTime date)
        {
            var absenceRequests = ctx.AbsenceRequests.Where(x => x.RequesterId == userId
                                                        && x.StartAbsenceDate.Date <= date.Date
                                                        && x.EndAbsenceDate >= date.Date).FirstOrDefault();

            return absenceRequests;
        }

        // Absecences returns are based on the Absence Start date
        public async Task<List<AbsenceRequest>> GetAbsenceRequestsByAbsenceTypeAndTime(int absenceTypeId,
                                                                     DateTime startDate, DateTime endDate)
        {
            // AbsenceType should match and Request StartDate needs to be between startDate and endDate
            var absenceRequests = await ctx.AbsenceRequests.Where(x => x.AbsenceTypeId == absenceTypeId &&
                                                    startDate.Date <= x.StartAbsenceDate.Date && 
                                                    endDate.Date >= x.StartAbsenceDate.Date &&
                                                    !x.Approved && !x.Rejected).ToListAsync();

            return absenceRequests;
        }

        public async Task<List<AbsenceRequest>> GetActiveAbsenceRequestsByUser(int userId)
        {
            // Active means not reviewed yet so not approved and not rejected
            var absenceRequests = await ctx.AbsenceRequests.Where(x => x.RequesterId == userId
                                                && !x.Approved && !x.Rejected).ToListAsync();

            return absenceRequests;
        }

        public void DeleteAbsenceRequest(int id)
        {
            var exist = ctx.AbsenceRequests.FirstOrDefault(x => x.AbsenceRequestId == id);

            if (exist != null)
            {
                ctx.AbsenceRequests.Remove(exist);

                ctx.SaveChanges();
            }
        }

        public void UpdateAbsenceRequest(AbsenceRequest absenceRequest)
        {
            var exist = ctx.AbsenceRequests.FirstOrDefault(x => x.AbsenceRequestId == absenceRequest.AbsenceRequestId);

            if (exist != null)
            {
                //exist.RequestName = AbsenceRequest.RequestName;

                ctx.SaveChanges();
            }
        }
    }
}
