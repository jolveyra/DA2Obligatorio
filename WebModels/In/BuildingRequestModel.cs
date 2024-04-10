using Domain;

namespace WebModels
{
    public class BuildingRequestModel
    {
        public string Name { get; set; }

        public Building ToEntity()
        {
            return new Building
            {
                Name = Name
            };
        }
    }
}