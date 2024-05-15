using Domain;

namespace WebModels.BuildingModels
{
    public class BuildingResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public float SharedExpenses { get; set; }
        public List<FlatResponseModel> Flats { get; set; }
        public string Street { get; set; }
        public int DoorNumber { get; set; }
        public string CornerStreet { get; set; }
        public string ConstructorCompany { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public BuildingResponseModel(Building building)
        {
            Id = building.Id;
            Name = building.Name;
            SharedExpenses = building.SharedExpenses;
            ConstructorCompany = building.ConstructorCompany;
            Street = building.Address.Street;
            DoorNumber = building.Address.DoorNumber;
            CornerStreet = building.Address.CornerStreet;
            Latitude = building.Address.Latitude;
            Longitude = building.Address.Longitude;
            Flats = new List<FlatResponseModel>();

        }

        public override bool Equals(object? obj)
        {
            return obj is BuildingResponseModel buildingResponseModel && Id == buildingResponseModel.Id;
        }

    }
}
