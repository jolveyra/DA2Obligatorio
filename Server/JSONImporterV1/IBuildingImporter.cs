namespace JSONImporterV1
{
    public interface IBuildingImporter
    {
        public List<DTOBuilding> ImportBuildingsFromFile(string path);
    }
}
