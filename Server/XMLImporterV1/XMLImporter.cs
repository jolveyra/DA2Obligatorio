using XMLImporterV1.Domain;
using System.Xml.Serialization;

namespace XMLImporterV1
{
    public class XMLImporter
    {
        public static List<DTOBuilding> ImportBuildings(string xmlFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Root));
            using (FileStream fileStream = new FileStream(xmlFilePath, FileMode.Open))
            {
                Root buildingData = (Root)serializer.Deserialize(fileStream);
                var buildings = new List<DTOBuilding>();
                EdificiosToDTOBuildings(buildingData, buildings);

                return buildings;
            }
        }

        private static void EdificiosToDTOBuildings(Root buildingData, List<DTOBuilding> buildings)
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
                    ManagerEmail = string.IsNullOrEmpty(edificio.Encargado) ? null : edificio.Encargado,
                    Flats = new List<DTOFlat>()
                };
                DepartamentosToDTOFlats(edificio, building);

                buildings.Add(building);
            }
        }

        private static void DepartamentosToDTOFlats(Edificio edificio, DTOBuilding building)
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
