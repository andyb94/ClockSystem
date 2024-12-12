using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IAbsenceRequestService
    {
        public AbsenceRequest GetAbsenceRequestById(int id);
        public bool AddAbsenceRequest(int userId, int absenceTypeId, DateTime startDate,
                                                DateTime endDate);
        public List<AbsenceRequest> GetAbsenceRequests();
        public bool CheckIfRequestExistForDates(int userId, DateTime startDate, DateTime endDate);
        public Task<List<AbsenceRequest>> GetAbsenceRequestsForAdminReview(int userId, int roleTypeId, int absenceTypeId,
                                                                     DateTime startDate, DateTime endDate);
        public Task<List<AbsenceRequest>> GetActiveAbsenceRequestsByUser(int userId);
        public void DeleteAbsenceRequest(int id);
        public void UpdateAbsenceRequest(int requestId, int userId, bool approved);
    }
}
