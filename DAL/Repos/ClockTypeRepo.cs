
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class ClockTypeRepo : IClockTypeRepo
    {
        // Using direct injection for DB context to repos. setting it at creation.
        private readonly AppDbContext ctx;
        public ClockTypeRepo(AppDbContext context) { 
            ctx = context;
        }

        public ClockType GetClockTypeById(int id)
        {
            var clockType = ctx.ClockTypes.FirstOrDefault(x => x.ClockTypeId == id);

            return clockType;
        }

        public void AddClockType(ClockType clockType)
        {
            var exist = ctx.ClockTypes.FirstOrDefault(x => x.ClockTypeId == clockType.ClockTypeId);

            if (exist == null)
            {
                ctx.ClockTypes.Add(clockType);

                ctx.SaveChanges();
            }
        }

        public List<ClockType> GetClockTypes()
        {
            var clockTypes = ctx.ClockTypes.ToList();

            return clockTypes;
        }

        public void DeleteClockType(int id)
        {
            var exist = ctx.ClockTypes.FirstOrDefault(x => x.ClockTypeId == id);

            if (exist != null)
            {
                ctx.ClockTypes.Remove(exist);

                ctx.SaveChanges();
            }
        }

        public void UpdateClockType(ClockType clockType)
        {
            var exist = ctx.ClockTypes.FirstOrDefault(x => x.ClockTypeId == clockType.ClockTypeId);

            if (exist != null)
            {
                exist.TypeName = clockType.TypeName;

                ctx.SaveChanges();
            }
        }
    }
}
