
using Domain;

namespace RepositoryInterfaces;

public interface IBuildingRepository
{
    public Building CreateBuilding(Building building);
    Flat CreateFlat(Flat flat);
    List<Flat> CreateFlats(List<Flat> flats);
    void DeleteBuilding(Building building);
    void DeleteFlats(List<Flat> flats);
    IEnumerable<Flat> GetAllBuildingFlats(Guid buildingId);
    public IEnumerable<Building> GetAllBuildings();
    List<Flat> GetAllFlats();
    Building GetBuildingById(Guid buildingId);
    Flat GetFlatByFlatId(Guid flatId);
    Building UpdateBuilding(Building building);
    Flat UpdateFlat(Flat flat);
}
