using Domain;

namespace WebModels.BuildingModels
{
    public class UpdateBuildingRequestModel
    {
        public float SharedExpenses { get; set; }
        public List<Guid> MaintenanceEmployees { get; set; }
        public string ConstructorCompany { get; set; }

        public Building ToEntity()
        {
            return new Building
            {
                SharedExpenses = SharedExpenses,
                ConstructorCompany = ConstructorCompany
            };
        }
    }
}