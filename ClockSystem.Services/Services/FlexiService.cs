using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;

namespace ClockSystem.Services.Services
{
    public class FlexiService : IFlexiService
    {
        private readonly IFlexiRepo _flexiRepo;
        public FlexiService(IFlexiRepo flexiRepo)
        {
            _flexiRepo = flexiRepo;
        }

        public FlexiTimeRecord GetFlexiRecordById(int id)
        {
            return _flexiRepo.GetFlexiRecordById(id);
        }

        public FlexiTimeRecord GetFlexiRecordByUserId(int id)
        {
            return _flexiRepo.GetFlexiRecordByUserId(id);
        }

        // Created when user is created
        public bool AddFlexiRecord(int userId, float flexiAmount) {
            // Can only have one record
            var exist = GetFlexiRecordByUserId(userId);

            if (exist != null) {
                return false;
            }
            // Prevent acquiring more than 7 hours of flexi
            if (flexiAmount > 7)
            {
                flexiAmount = 7;
            }

            _flexiRepo.AddFlexiRecord(new FlexiTimeRecord
            {
                FlexiTime = flexiAmount,
                UserId = userId
            });

            return true;
        }

        public List<FlexiTimeRecord> GetFlexiRecords() { 
            return _flexiRepo.GetFlexiRecords();
        }

        public void DeleteFlexiRecord(int id)
        {
            _flexiRepo.DeleteFlexiRecord(id);
        }

        // Gets updated when clocking 
        public bool UpdateFlexiRecord(int userId, float flexiAmount)
        {
            var record = _flexiRepo.GetFlexiRecordByUserId(userId);

            if (record != null)
            {
                record.FlexiTime += flexiAmount;
                // prevent more than 7 hours total
                if (record.FlexiTime > 7)
                {
                    record.FlexiTime = 7;
                }

                _flexiRepo.UpdateFlexiRecord(record);

                return true;
            }

            return false;
        }
    }
}
