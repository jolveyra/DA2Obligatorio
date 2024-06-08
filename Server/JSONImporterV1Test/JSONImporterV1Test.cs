using JSONImporterV1;
using JSONImporterV1.Domain;
using System.Xml.Serialization;
using Moq;
using JSONImporterV1.Deserializer;
using BuildingImporter;

namespace JSONImporterV1Test
{
    [TestClass]
    public class JSONImporterV1Test
    {
        private JsonImporter importer;
        string path;

        [TestInitialize]
        public void Initialize()
        {
            path = @"..\..\..\BuildingsJsonStub.json";
            importer = new JsonImporter();
        }

        [TestMethod]
        public void ImportBuildingsFromFileTestOk()
        {

            List<DTOBuilding> buildings = new List<DTOBuilding>();
            List<Edificio> edificios = new List<Edificio>() {
                new Edificio()
                {
                    Nombre = "Las torres",
                    Direccion = new Direccion
                    {
                        Calle_principal = "Av. 6 de Diciembre",
                        Numero_puerta = 3030,
                        Calle_secundaria = "Av. Eloy Alfaro"
                    },
                    Encargado = "unjuan.perez@gmail.com",
                    Gps = new Gps
                    {
                        Latitud = -0.176,
                        Longitud = -78.48
                    },
                    GastosComunes = 5000f,
                    Departamentos = new List<Departamento>
                    {
                        new Departamento
                        {
                            Piso = 1,
                            Numero_puerta = 101,
                            Habitaciones = 3,
                            ConTerraza = false,
                            Baños = 2,
                            PropietarioEmail = "juan.perez@gmail.com"
                        }
                    }
                }
            };

            Root rootBuildingData = new Root() { Edificios = edificios };

            buildings = importer.ImportBuildingsFromFile(path);

            Assert.AreEqual(rootBuildingData.Edificios[0].Nombre, buildings[0].Name);

        }
    }
}