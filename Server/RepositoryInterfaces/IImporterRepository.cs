using Domain;

namespace RepositoryInterfaces
{
    public interface IImporterRepository
    {
        Importer CreateImporter(Importer importer);
        IEnumerable<Importer> GetAllImporters();
        Importer GetImporterByName(string name);
    }
}