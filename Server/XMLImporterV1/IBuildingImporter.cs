namespace XMLImporterV1
{
    public interface IBuildingImporter
    {
        public List<DTOBuilding> ImportBuildingsFromFile(string path);
    }
}
