using Domain;
using System.Collections.Generic;

namespace WebModels
{
    public class BuildingRequestModel
    {
        public string Name { get; set; }

        public int Flats { get; set; }

        public Building ToEntity()
        {
            List<Flat> _flats = CreateBuildingFlats();

            return new Building
            {
                Name = Name,
                Flats = _flats
            };
        }

        private List<Flat> CreateBuildingFlats()
        {
            List<Flat> _flats = new List<Flat>();
            for (int i = 0; i < Flats; i++)
            {
                Flat flat = new Flat
                {
                    Id = Guid.NewGuid()
                };

                _flats.Add(flat);
            }

            return _flats;
        }
    }
}