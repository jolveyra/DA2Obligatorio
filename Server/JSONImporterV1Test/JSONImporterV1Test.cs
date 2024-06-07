using JSONImporterV1;
using JSONImporterV1.Domain;
using System.Xml.Serialization;
using Moq;
using JSONImporterV1.Deserializer;

namespace JSONImporterV1Test
{
    [TestClass]
    public class JSONImporterV1Test
    {

        private Mock<IJSONDeserializer> deserializerMock;
        private JsonImporter importer;
        string path;

        [TestInitialize]
        public void Initialize()
        {
            deserializerMock = new Mock<IJSONDeserializer>(MockBehavior.Strict);
            path = @"..\..\buildings.json";
            importer = new JsonImporter(deserializerMock.Object);
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
                        CallePrincipal = "Av. 6 de Diciembre",
                        NumeroPuerta = 3030,
                        CalleSecundaria = "Av. Eloy Alfaro"
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
                            NumeroPuerta = 101,
                            Habitaciones = 3,
                            ConTerraza = false,
                            Banos = 2,
                            PropietarioEmail = "juan.perez@gmail.com"
                        }
                    }
                }
            };

            Root rootBuildingData = new Root() { Edificios = edificios };
            deserializerMock.Setup(s => s.Deserialize<Root>(It.IsAny<string>())).Returns(rootBuildingData);

            buildings = importer.ImportBuildingsFromFile(path);

            Assert.AreEqual(rootBuildingData.Edificios[0].Nombre, buildings[0].Name);

        }
    }
}