using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class AbsenceTypeRepo : IAbsenceTypeRepo
    {
        // Using direct injection for DB context to repos. setting it at creation.
        private readonly AppDbContext ctx;
        public AbsenceTypeRepo(AppDbContext context) { 
            ctx = context;
        }

        public AbsenceType GetAbsenceTypeById(int id)
        {
            var absenceType = ctx.AbsenceTypes.FirstOrDefault(x => x.AbsenceTypeId == id);

            return absenceType;
        }

        public void AddAbsenceType(AbsenceType absenceType)
        {
            var exist = ctx.AbsenceTypes.FirstOrDefault(x => x.AbsenceTypeId == absenceType.AbsenceTypeId);

            if (exist == null)
            {
                ctx.AbsenceTypes.Add(absenceType);

                ctx.SaveChanges();
            }
        }

        public List<AbsenceType> GetAbsenceTypes()
        {
            var absenceTypes = ctx.AbsenceTypes.ToList();

            return absenceTypes;
        }

        public void DeleteAbsenceType(int id)
        {
            var exist = ctx.AbsenceTypes.FirstOrDefault(x => x.AbsenceTypeId == id);

            if (exist != null)
            {
                ctx.AbsenceTypes.Remove(exist);

                ctx.SaveChanges();
            }
        }

        public void UpdateAbsenceType(AbsenceType absenceType)
        {
            var exist = ctx.AbsenceTypes.FirstOrDefault(x => x.AbsenceTypeId == absenceType.AbsenceTypeId);

            if (exist != null)
            {
                exist.AbsenceTypeName = absenceType.AbsenceTypeName;

                ctx.SaveChanges();
            }
        }
    }
}
