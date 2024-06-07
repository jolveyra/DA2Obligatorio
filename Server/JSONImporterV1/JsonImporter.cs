using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JSONImporterV1.Domain;

namespace BuildingImporter
{
    public class JsonImporter : IBuildingImporter
    {
        public List<DTOBuilding> ImportBuildingsFromFile(string path)
        {
            string json = File.ReadAllText(path);
            var buildingData = JsonConvert.DeserializeObject<Root>(json);
            var buildings = new List<DTOBuilding>();
            ConvertEdificiosToDTOBuildings(buildingData, buildings);

            return buildings;
        }

        private void ConvertEdificiosToDTOBuildings(Root? buildingData, List<DTOBuilding> buildings)
        {
            foreach (var edificio in buildingData.Edificios)
            {
                var building = new DTOBuilding
                {
                    Name = edificio.Nombre,
                    SharedExpenses = edificio.GastosComunes,
                    Street = edificio.Direccion.CallePrincipal,
                    Number = edificio.Direccion.NumeroPuerta,
                    CornerStreet = edificio.Direccion.CalleSecundaria,
                    Latitude = edificio.Gps.Latitud,
                    Longitude = edificio.Gps.Longitud,
                    ManagerEmail = edificio.Encargado,
                    Flats = new List<DTOFlat>()
                };
                ConvertDepartamentosToDTOFlats(edificio, building);

                buildings.Add(building);
            }
        }

        private void ConvertDepartamentosToDTOFlats(Edificio edificio, DTOBuilding building)
        {
            foreach (var departamento in edificio.Departamentos)
            {
                var flat = new DTOFlat
                {
                    Number = departamento.NumeroPuerta,
                    Floor = departamento.Piso,
                    OwnerEmail = departamento.PropietarioEmail,
                    Rooms = departamento.Habitaciones,
                    Bathrooms = departamento.Banos,
                    HasBalcony = departamento.ConTerraza
                };

                building.Flats.Add(flat);
            }
        }
    }
}
