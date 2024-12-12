using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("ClockType")]
    public class ClockType
    {
        public int ClockTypeId { get; set; }
        public string TypeName { get; set; }
    }

    public class ClockTypeConfig : IEntityTypeConfiguration<ClockType>
    {
        public void Configure(EntityTypeBuilder<ClockType> builder)
        {
            builder.HasKey(x => x.ClockTypeId);
            builder.Property(x => x.TypeName);
        }
    }
}
