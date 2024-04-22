using Domain;
using System.Collections.Generic;

namespace WebModels.BuildingModels
{
    public class BuildingRequestModel
    {
        public string Name { get; set; }
        public int Flats { get; set; }
        public string Direction { get; set; }
        public string ConstructorCompany { get; set; }
        public float SharedExpenses { get; set; }

        public Building ToEntity()
        {

            return new Building
            {
                Name = Name,
                Direction = Direction,
                ConstructorCompany = ConstructorCompany,
                SharedExpenses = SharedExpenses
            };
        }
    }
}