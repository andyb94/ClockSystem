using DAL.Entities;

namespace ClockSystem.Services.Models
{
    public class WeeklyClockRecords
    {
        public List<ClockRecord> MonRecords { get; set; }
        public List<ClockRecord> TueRecords { get; set; }
        public List<ClockRecord> WedRecords { get; set; }
        public List<ClockRecord> ThurRecords { get; set; }
        public List<ClockRecord> FriRecords { get; set; }
        public List<ClockRecord> SatRecords { get; set; }
        public List<ClockRecord> SunRecords { get; set; }
    }
}
