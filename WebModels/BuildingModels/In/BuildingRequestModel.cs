using Domain;
using System.Collections.Generic;

namespace WebModels.BuildingModels
{
    public class BuildingRequestModel
    {
        public string Name { get; set; }
        public int Flats { get; set; }
        public string Street { get; set; }
        public int DoorNumber { get; set; }
        public string CornerStreet { get; set; }
        public string ConstructorCompany { get; set; }
        public float SharedExpenses { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Building ToEntity()
        {

            return new Building
            {
                Name = Name,
                Street = Street,
                DoorNumber = DoorNumber,
                CornerStreet = CornerStreet,
                ConstructorCompany = ConstructorCompany,
                SharedExpenses = SharedExpenses,
                Latitude = Latitude,
                Longitude = Longitude
            };
        }
    }
}