using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;

namespace ClockSystem.Services.Services
{
    public class AbsenceRequestService : IAbsenceRequestService
    {
        private readonly IAbsenceRequestRepo _absenceRequestRepo;
        private readonly IAbsenceRecordService _absenceRecordService;
        private readonly IUserRepo _userRepo;
        private readonly IFlexiService _flexiService;

        public AbsenceRequestService(IAbsenceRequestRepo absenceRequestRepo, 
            IUserRepo userRepo, IAbsenceRecordService absenceRecordService, IFlexiService flexiService)
        {
            _absenceRequestRepo = absenceRequestRepo;
            _userRepo = userRepo;
            _absenceRecordService = absenceRecordService;
            _flexiService = flexiService;
        }

        public AbsenceRequest GetAbsenceRequestById(int id)
        {
            return _absenceRequestRepo.GetAbsenceRequestById(id);
        }

        public bool AddAbsenceRequest(int userId, int absenceTypeId, DateTime startDate,
                                                DateTime endDate) {

            AbsenceRequest absenceRequest = new AbsenceRequest
            {
                AbsenceTypeId = absenceTypeId,
                StartAbsenceDate = startDate,
                EndAbsenceDate = endDate,
                RequesterId = userId,
                Approved = false,
                Rejected = false
            };

            //Flexi
            if (absenceTypeId == 3)
            {
                // If flexi doesn't exist or the hours are not over 7 then don't allow
                // Need 7 hours to make this request
                var flexi = _flexiService.GetFlexiRecordByUserId(userId);
                if (flexi == null || flexi.FlexiTime < 7) {
                    return false;
                }
            }

            _absenceRequestRepo.AddAbsenceRequest(absenceRequest);
            return true;
        }

        public List<AbsenceRequest> GetAbsenceRequests() { 
            return _absenceRequestRepo.GetAbsenceRequests();
        }

        public bool CheckIfRequestExistForDates(int userId, DateTime startDate, DateTime endDate)
        {
            bool exist = false;
            DateTime date = startDate;

            // Loop while exist is false and date does not exceed endDate
            while (!exist && date.Date <= endDate.Date)
            {
                // Get request for user and date
                var request = _absenceRequestRepo.GetAbsenceRequestByUserAndDate(userId, date);

                // if not null means exist, will break loop
                if (request != null)
                {
                    exist = true;
                }

                date = date.AddDays(1);
            }

            return exist;
        }

        public async Task<List<AbsenceRequest>> GetAbsenceRequestsForAdminReview(int userId, int roleTypeId, int absenceTypeId,
                                                                     DateTime startDate, DateTime endDate)
        {
            // get records by absence type that start between startDate and endDate
            var records = await _absenceRequestRepo.GetAbsenceRequestsByAbsenceTypeAndTime(absenceTypeId, startDate, endDate);

            // get users of roleType then extract just the userIds, exclude current user (can't review own requests)
            var users = await _userRepo.GetUsersByRole(roleTypeId);
            var userIds = users.Where(x => x.UserId != userId).Select(x => x.UserId);


            // filter records by only users in the requested role type
            var filteredRecords = records.Where(x => userIds.Contains(x.RequesterId)).ToList();

            return filteredRecords;
        }

        public async Task<List<AbsenceRequest>> GetActiveAbsenceRequestsByUser(int userId)
        {
            return await _absenceRequestRepo.GetActiveAbsenceRequestsByUser(userId);
        }

        public void DeleteAbsenceRequest(int id)
        {
            _absenceRequestRepo.DeleteAbsenceRequest(id);
        }

        public void UpdateAbsenceRequest(int requestId, int userId, bool approved)
        {
            AbsenceRequest absenceRequest = GetAbsenceRequestById(requestId);

            absenceRequest.Approved = approved;
            absenceRequest.Rejected = !approved;
            absenceRequest.ReviewerId = userId;

            _absenceRequestRepo.UpdateAbsenceRequest(absenceRequest);

            if (approved)
            {
                _absenceRecordService.AddAbsenceRecords(absenceRequest);
            }
        }
    }
}
