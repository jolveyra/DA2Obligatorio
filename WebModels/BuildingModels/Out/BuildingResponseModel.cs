using Domain;

namespace WebModels.BuildingModels
{
    public class BuildingResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public BuildingResponseModel(Building building)
        {
            Id = building.Id;
            Name = building.Name;
        }

    }
}
