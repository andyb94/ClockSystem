
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IAbsenceRequestRepo
    {
        public AbsenceRequest GetAbsenceRequestById(int id);
        public AbsenceRequest GetAbsenceRequestByUserAndDate(int userId, DateTime date);
        public void AddAbsenceRequest(AbsenceRequest absenceRequest);
        public List<AbsenceRequest> GetAbsenceRequests();
        public Task<List<AbsenceRequest>> GetAbsenceRequestsByAbsenceTypeAndTime(int absenceTypeId,
                                                                     DateTime startDate, DateTime endDate);
        public Task<List<AbsenceRequest>> GetActiveAbsenceRequestsByUser(int userId);
        public void DeleteAbsenceRequest(int id);
        public void UpdateAbsenceRequest(AbsenceRequest absenceRequest);
    }
}
