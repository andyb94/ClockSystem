using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IFlexiService
    {
        public FlexiTimeRecord GetFlexiRecordById(int id);
        public FlexiTimeRecord GetFlexiRecordByUserId(int id);
        public bool AddFlexiRecord(int userId, float flexiAmount);
        public List<FlexiTimeRecord> GetFlexiRecords();
        public void DeleteFlexiRecord(int id);
        public bool UpdateFlexiRecord(int userId, float flexiAmount);
    }
}
