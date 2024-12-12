using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;

namespace ClockSystem.Services.Services
{
    public class AbsenceRecordService : IAbsenceRecordService
    {
        private readonly IAbsenceRecordRepo _absenceRecordRepo;
        private readonly IFlexiService _flexiService;
        public AbsenceRecordService(IAbsenceRecordRepo absenceRecordRepo, IFlexiService flexiService)
        {
            _absenceRecordRepo = absenceRecordRepo;
            _flexiService = flexiService;
        }

        public AbsenceRecord GetAbsenceRecordById(int id)
        {
            return _absenceRecordRepo.GetAbsenceRecordById(id);
        }

        // Add records for each date in the request between start and end dates
        public void AddAbsenceRecords(AbsenceRequest absenceRequest) {
            var date = absenceRequest.StartAbsenceDate;

            while (date.Date <= absenceRequest.EndAbsenceDate)
            {
                _absenceRecordRepo.AddAbsenceRecord(new AbsenceRecord
                {
                    AbsenceDate = date,
                    AbsenceRequestId = absenceRequest.AbsenceRequestId
                });

                date = date.AddDays(1);
            }

            // Flexi
            if (absenceRequest.AbsenceTypeId == 3)
            {
                // Get record then update by deducting 7 hours
                var flexi = _flexiService.GetFlexiRecordByUserId(absenceRequest.RequesterId);
                _flexiService.UpdateFlexiRecord(flexi.FlexiTimeRecordId, -7);
            }
        }

        public List<AbsenceRecord> GetAbsenceRecords() { 
            return _absenceRecordRepo.GetAbsenceRecords();
        }

        public void DeleteAbsenceRecord(int id)
        {
            _absenceRecordRepo.DeleteAbsenceRecord(id);
        }

        public void UpdateAbsenceRecord(AbsenceRecord absenceRecord)
        {
            _absenceRecordRepo.UpdateAbsenceRecord(absenceRecord);
        }
    }
}
