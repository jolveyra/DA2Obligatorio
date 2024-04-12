using Domain;

namespace WebModels
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

        public override bool Equals(object? obj)
        {
            return obj is BuildingResponseModel buildingResponseModel && Id == buildingResponseModel.Id;
        }

    }
}
