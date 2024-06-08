using XMLImporterV1;
using XMLImporterV1.Domain;
using BuildingImporter;
using System.Xml.Serialization;
using Moq;

namespace XMLImporterV1Test
{
    [TestClass]
    public class XMLImporterV1Test
    {
        XMLImporter importer;
        string path;

        [TestInitialize]
        public void Initialize()
        {
            importer = new XMLImporter();
            path = @"..\..\BuildingsFileStub.xml";

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

            buildings = importer.ImportBuildingsFromFile(path);

            Assert.AreEqual(rootBuildingData.Edificios[0].Nombre, buildings[0].Name);

        }
    }
}