using DAL.Entities;
using DAL.Interfaces;
using ClockSystem.Services.Interfaces;

namespace ClockSystem.Services.Services
{
    public class ClockTypeService : IClockTypeService
    {
        private readonly IClockTypeRepo _clockTypeRepo;
        public ClockTypeService(IClockTypeRepo clockTypeRepo)
        {
            _clockTypeRepo = clockTypeRepo;
        }

        public ClockType GetClockTypeById(int id)
        {
            return _clockTypeRepo.GetClockTypeById(id);
        }

        public void AddClockType(ClockType clockType) { 
            _clockTypeRepo.AddClockType(clockType);
        }

        public List<ClockType> GetClockTypes() { 
            return _clockTypeRepo.GetClockTypes();
        }

        public void DeleteClockType(int id)
        {
            _clockTypeRepo.DeleteClockType(id);
        }

        public void UpdateClockType(ClockType clockType)
        {
            _clockTypeRepo.UpdateClockType(clockType);
        }
    }
}
