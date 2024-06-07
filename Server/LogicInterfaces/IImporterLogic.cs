using Domain;

namespace LogicInterfaces
{
    public interface IImporterLogic
    {
        IEnumerable<Importer> GetAllImporters();
    }
}