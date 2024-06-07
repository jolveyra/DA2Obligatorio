using Domain;

namespace LogicInterfaces
{
    public interface IImporterLogic
    {
        Importer CreateImporter(Importer importer);
        IEnumerable<Importer> GetAllImporters();
    }
}