using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("ClockRecord")]
    public class ClockRecord
    {
        public int ClockRecordId { get; set; }
        public int ClockTypeId { get; set; }
        public DateTime ClockTime { get; set; }
        public bool ClockedIn { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int DayClockOrder { get; set; }
    }

    public class ClockRecordConfig : IEntityTypeConfiguration<ClockRecord>
    {
        public void Configure(EntityTypeBuilder<ClockRecord> builder)
        {
            builder.HasKey(x => x.ClockRecordId);
            builder.Property(x => x.ClockTime);
            builder.Property(x => x.ClockTypeId);
            builder.Property(x => x.ClockedIn);
            builder.Property(x => x.UserId);
            builder.Property(x => x.DayClockOrder);
        }
    }
}
