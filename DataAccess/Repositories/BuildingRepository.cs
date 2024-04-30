using Domain;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces;

namespace DataAccess.Repositories
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
            return _context.Buildings.Include(b=>b.Address);
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
            _context.Remove(building.Address);
            _context.Buildings.Remove(building);
            _context.SaveChanges();
        }

        public Building GetBuildingById(Guid guid)
        {

            Building building = _context.Buildings.Include(b => b.Address).FirstOrDefault(b => b.Id.Equals(guid));

            if (building is null)
            {
                throw new ArgumentException("Building not found");
            }

            return building;

        }

        public Flat GetFlatByFlatId(Guid flatId)
        {
            Flat flat = _context.Flats.FirstOrDefault(f => f.Id.Equals(flatId));

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

        public IEnumerable<Flat> GetAllBuildingFlats(Guid buildingId)
        {
            return _context.Flats.Where(f => f.Building.Id.Equals(buildingId));
        }

        public Flat CreateFlat(Flat flat)
        {
            _context.Flats.Add(flat);
            _context.SaveChanges();
            return flat;
        }

        public List<Flat> GetAllFlats()
        {
            return _context.Flats.ToList();
        }

        public void DeleteFlats(List<Flat> flats)
        {
            _context.Flats.RemoveRange(flats);
            _context.SaveChanges();
        }
    }
}
