using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("AbsenceRequests")]
    public class AbsenceRequest
    {
        public int AbsenceRequestId { get; set; }
        public int AbsenceTypeId { get; set; }
        public DateTime StartAbsenceDate { get; set; }
        public DateTime EndAbsenceDate { get; set; }
        public int RequesterId { get; set; }
        public int? ReviewerId { get; set; }
        public bool Approved { get; set; }
        public bool Rejected { get; set; }
    }

    public class AbsenceRequestConfig : IEntityTypeConfiguration<AbsenceRequest>
    {
        public void Configure(EntityTypeBuilder<AbsenceRequest> builder)
        {
            builder.HasKey(x => x.AbsenceRequestId);
            builder.Property(x => x.AbsenceTypeId);
            builder.Property(x => x.StartAbsenceDate);
            builder.Property(x => x.EndAbsenceDate);
            builder.Property(x => x.RequesterId);
            builder.Property(x => x.ReviewerId);
            builder.Property(x => x.Approved);
            builder.Property(x => x.Rejected);
        }
    }
}
