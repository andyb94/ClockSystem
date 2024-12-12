
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IClockTypeRepo
    {
        public ClockType GetClockTypeById(int id);
        public void AddClockType(ClockType clockType);
        public List<ClockType> GetClockTypes();
        public void DeleteClockType(int id);
        public void UpdateClockType(ClockType clockType);
    }
}
