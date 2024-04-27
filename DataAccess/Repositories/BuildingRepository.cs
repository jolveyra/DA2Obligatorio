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

        public Building UpdateBuilding(Building building)
        {
            _context.Update(building);
            _context.SaveChanges();

            return building;
        }

        public void DeleteBuilding(Building building)
        {
            _context.Buildings.Remove(building);
            _context.SaveChanges();
        }

        public Building GetBuildingById(Guid guid)
        {

            Building building = _context.Buildings.FirstOrDefault(b => b.Id.Equals(guid));

            if (building is null)
            {
                throw new ArgumentException("Building not found");
            }

            return building;

        }

        public Flat GetFlatByBuildingAndFlatId(Guid buildingId, Guid flatId)
        {
            Flat flat = GetBuildingById(buildingId).Flats.FirstOrDefault(f => f.Id.Equals(flatId));

            if (flat is null)
            {
                throw new ArgumentException("Flat not found");
            }

            return flat;
        }

        public Flat UpdateFlat(Flat flat)
        {
            _context.Update(flat);
            _context.SaveChanges();
            return flat;
        }
    }
}
