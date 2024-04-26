using Domain;
using DataAccess.Context;
using RepositoryInterfaces;

namespace DataAccess
{

    public class BuildingRepository: IBuildingRepository
    {

        private readonly BuildingBossContext _context;

        public BuildingRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public IEnumerable<Building> GetAllBuildings()
        {
            return _context.Buildings;
        }

        public Building CreateBuilding(Building building)
        {
            _context.Buildings.Add(building);
            _context.SaveChanges();
            return building;
        }

        public Building UpdateBuilding(Guid id, float sharedExpenses)
        {
            Building building = _context.Buildings.FirstOrDefault(b => b.Id.Equals(id));

            building.SharedExpenses = sharedExpenses;

            _context.Update(building);
            _context.SaveChanges();

            return building;
        }

        void IBuildingRepository.DeleteBuilding(Guid guid)
        {
            throw new NotImplementedException();
        }

        Building IBuildingRepository.GetBuildingById(Guid guid)
        {
            throw new NotImplementedException();
        }

        Flat IBuildingRepository.GetFlatByBuildingAndFlatId(Guid guid1, Guid guid2)
        {
            throw new NotImplementedException();
        }

        Flat IBuildingRepository.UpdateFlat(Guid guid1, Guid guid2, Flat flat)
        {
            throw new NotImplementedException();
        }
    }
}
