using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<ClockRecord> ClockRecords { get; set; }
        public virtual DbSet<ClockType> ClockTypes { get; set; }
        public virtual DbSet<AbsenceRequest> AbsenceRequests { get; set; }
        public virtual DbSet<AbsenceType> AbsenceTypes { get; set; }
        public virtual DbSet<AbsenceRecord> AbsenceRecords { get; set; }
        public virtual DbSet<FlexiTimeRecord> FlexiTimeRecords { get; set; }

        // Adding configurations of models to map to entity framework
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClockRecordConfig());
            modelBuilder.ApplyConfiguration(new ClockTypeConfig());
            modelBuilder.ApplyConfiguration(new AbsenceRequestConfig());
            modelBuilder.ApplyConfiguration(new AbsenceTypeConfig());
            modelBuilder.ApplyConfiguration(new AbsenceRecordConfig());
            modelBuilder.ApplyConfiguration(new FlexiTimeRecordConfig());
        }
    }
}
