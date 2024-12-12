
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IFlexiRepo
    {
        public FlexiTimeRecord GetFlexiRecordById(int id);
        public FlexiTimeRecord GetFlexiRecordByUserId(int id);
        public void AddFlexiRecord(FlexiTimeRecord flexiRecord);
        public List<FlexiTimeRecord> GetFlexiRecords();
        public void DeleteFlexiRecord(int id);
        public void UpdateFlexiRecord(FlexiTimeRecord flexiRecord);
    }
}
