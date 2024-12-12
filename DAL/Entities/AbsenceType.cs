using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("AbsenceType")]
    public class AbsenceType
    {
        public int AbsenceTypeId { get; set; }
        public string AbsenceTypeName { get; set; }
    }

    public class AbsenceTypeConfig : IEntityTypeConfiguration<AbsenceType>
    {
        public void Configure(EntityTypeBuilder<AbsenceType> builder)
        {
            builder.HasKey(x => x.AbsenceTypeId);
            builder.Property(x => x.AbsenceTypeName);
        }
    }
}
