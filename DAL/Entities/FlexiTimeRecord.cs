using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("FlexiTimeRecord")]
    public class FlexiTimeRecord
    {
        public int FlexiTimeRecordId { get; set; }
        public double FlexiTime { get; set; }
        public int UserId { get; set; }
    }

    public class FlexiTimeRecordConfig : IEntityTypeConfiguration<FlexiTimeRecord>
    {
        public void Configure(EntityTypeBuilder<FlexiTimeRecord> builder)
        {
            builder.HasKey(x => x.FlexiTimeRecordId);
            builder.Property(x => x.FlexiTime);
            builder.Property(x => x.UserId);
        }
    }
}
