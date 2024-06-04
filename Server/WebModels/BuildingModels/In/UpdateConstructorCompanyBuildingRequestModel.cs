using Domain;

namespace WebModels.BuildingModels
{
    public class UpdateConstructorCompanyBuildingRequestModel
    {
        public string Name { get; set; }
        public Guid ManagerId { get; set; }

        public Building ToEntity()
        {
            return new Building { Name = Name, Manager = new User() { Id = ManagerId } };
        }
    }
}
