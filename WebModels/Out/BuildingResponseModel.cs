using Domain;

namespace WebModels
{
    public class BuildingResponseModel
    {
        public string Name { get; set; }

        public BuildingResponseModel(Building building)
        {
            Name = building.Name;
        }

    }
}
