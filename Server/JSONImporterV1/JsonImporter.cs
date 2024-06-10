using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSONImporterV1.Domain;
using BuildingImporter;
using JSONImporterV1.Deserializer;

namespace JSONImporterV1
{
    public class JsonImporter : IBuildingImporter
    {
        public List<DTOBuilding> ImportBuildingsFromFile(string path)
        {
            IJSONDeserializer _jsonDeserializer = new JSONDeserializer();
            Root buildingData = _jsonDeserializer.Deserialize<Root>(path);
            List<DTOBuilding> buildings = new List<DTOBuilding>();
            ConvertEdificiosToDTOBuildings(buildingData, buildings);

            return buildings;
        }

        private void ConvertEdificiosToDTOBuildings(Root? buildingData, List<DTOBuilding> buildings)
        {
            foreach (var edificio in buildingData.Edificios)
            {
                DTOBuilding building = new DTOBuilding
                {
                    Name = edificio.Nombre,
                    SharedExpenses = edificio.GastosComunes,
                    Street = edificio.Direccion.Calle_principal,
                    Number = edificio.Direccion.Numero_puerta,
                    CornerStreet = edificio.Direccion.Calle_secundaria,
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
                DTOFlat flat = new DTOFlat
                {
                    Number = departamento.Numero_puerta,
                    Floor = departamento.Piso,
                    OwnerEmail = departamento.PropietarioEmail,
                    Rooms = departamento.Habitaciones,
                    Bathrooms = departamento.Baños,
                    HasBalcony = departamento.ConTerraza
                };

                building.Flats.Add(flat);
            }
        }
    }
}