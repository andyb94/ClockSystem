using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    // Need to declare as int or IdentityUser will use string for UserId and cause conflict
    [Table("Users")]
    public class User
    {        
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role UserRole { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.FirstName);
            builder.Property(x => x.SecondName);
            builder.Property(x => x.Email);
            builder.Property(x => x.Password);
            builder.Property(x => x.RoleId);
            builder.Property(x => x.IsDeleted);
        }
    }
}
