using Domain;

namespace WebModels.BuildingModels
{
    public class BuildingResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public float SharedExpenses { get; set; }
        public List<FlatResponseModel> Flats { get; set; }

        public BuildingResponseModel(Building building)
        {
            Id = building.Id;
            Name = building.Name;
            SharedExpenses = building.SharedExpenses;
            if(!(building.Flats is null))
            {
                Flats = building.Flats.Select(flat => new FlatResponseModel(flat)).ToList();
            }

        }

        public override bool Equals(object? obj)
        {
            return obj is BuildingResponseModel buildingResponseModel && Id == buildingResponseModel.Id;
        }

    }
}
