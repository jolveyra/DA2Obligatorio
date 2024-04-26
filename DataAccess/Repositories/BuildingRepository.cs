using Domain;
using DataAccess.Context;
using IDataAccess;

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
    }
}
