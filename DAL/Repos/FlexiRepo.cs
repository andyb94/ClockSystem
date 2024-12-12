
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class FlexiRepo : IFlexiRepo
    {
        // Using direct injection for DB context to repos. setting it at creation.
        private readonly AppDbContext ctx;
        public FlexiRepo(AppDbContext context) { 
            ctx = context;
        }

        // Get Record by Id. The id passed in is to match FlexiRecordId
        public FlexiTimeRecord GetFlexiRecordById(int id)
        {            
            FlexiTimeRecord flexi = ctx.FlexiTimeRecords.FirstOrDefault(x => x.FlexiTimeRecordId == id);

            return flexi;
        }

        // Get Record by UserId. The id passed in is to match UserId
        public FlexiTimeRecord GetFlexiRecordByUserId(int id)
        {
            FlexiTimeRecord flexi = ctx.FlexiTimeRecords.FirstOrDefault(x => x.UserId == id);

            return flexi;
        }

        public void AddFlexiRecord(FlexiTimeRecord flexiRecord)
        {
            ctx.FlexiTimeRecords.Add(flexiRecord);
            ctx.SaveChanges();
        }

        public List<FlexiTimeRecord> GetFlexiRecords()
        { 
            List<FlexiTimeRecord> records = ctx.FlexiTimeRecords.ToList();

            return records;
        }

        public void DeleteFlexiRecord(int id)
        {
            var flexi = ctx.FlexiTimeRecords.FirstOrDefault(x => x.FlexiTimeRecordId == id);
            if (flexi != null) { 
                ctx.FlexiTimeRecords.Remove(flexi);
                ctx.SaveChanges();
            }
        }

        public void UpdateFlexiRecord(FlexiTimeRecord flexi) { 
            var exist = ctx.FlexiTimeRecords.FirstOrDefault(x => x.FlexiTimeRecordId == flexi.FlexiTimeRecordId);

            if (exist != null)
            {
                exist.FlexiTime = flexi.FlexiTime;

                ctx.SaveChanges();
            }
        }
    }
}
