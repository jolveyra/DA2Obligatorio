using Domain;

namespace WebModels.BuildingModels
{
    public class BuildingWithoutFlatsResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public float SharedExpenses { get; set; }
        public int AmountOfFlats { get; set; }
        public string Street { get; set; }
        public int DoorNumber { get; set; }
        public string CornerStreet { get; set; }
        public Guid ConstructorCompanyId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public BuildingWithoutFlatsResponseModel(Building building)
        {
            Id = building.Id;
            Name = building.Name;
            SharedExpenses = building.SharedExpenses;
            ConstructorCompanyId = building.ConstructorCompanyId;
            Street = building.Address.Street;
            DoorNumber = building.Address.DoorNumber;
            CornerStreet = building.Address.CornerStreet;
            Latitude = building.Address.Latitude;
            Longitude = building.Address.Longitude;
        }

        public override bool Equals(object? obj)
        {
            return obj is BuildingWithoutFlatsResponseModel buildingResponseModel && Id == buildingResponseModel.Id;
        }
    }
}