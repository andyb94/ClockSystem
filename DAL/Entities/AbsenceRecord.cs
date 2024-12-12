using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("AbsenceRecords")]
    public class AbsenceRecord
    {
        public int AbsenceRecordId { get; set; }        
        public DateTime AbsenceDate { get; set; }
        public int AbsenceRequestId { get; set; }
    }

    public class AbsenceRecordConfig : IEntityTypeConfiguration<AbsenceRecord>
    {
        public void Configure(EntityTypeBuilder<AbsenceRecord> builder)
        {
            builder.HasKey(x => x.AbsenceRecordId);
            builder.Property(x => x.AbsenceDate);
            builder.Property(x => x.AbsenceRequestId);
        }
    }
}
