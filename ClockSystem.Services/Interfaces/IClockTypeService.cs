using DAL.Entities;

namespace ClockSystem.Services.Interfaces
{
    public interface IClockTypeService
    {
        public ClockType GetClockTypeById(int id);
        public void AddClockType(ClockType clockType);
        public List<ClockType> GetClockTypes();
        public void DeleteClockType(int id);
        public void UpdateClockType(ClockType clockType);
    }
}
