using Domain;

namespace WebModels.BuildingModels
{
    public class UpdateBuildingRequestModel
    {
        public float SharedExpenses { get; set; }
        public List<Guid> MaintenanceEmployees { get; set; }
    }
}