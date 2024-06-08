using DataAccess.Context;
using Domain;
using RepositoryInterfaces;
using CustomExceptions;

namespace DataAccess.Repositories
{
    public class ImporterRepository: IImporterRepository
    {
        private readonly BuildingBossContext _context;

        public ImporterRepository(BuildingBossContext context)
        {
            _context = context;
        }


        public Importer CreateImporter(Importer importer)
        {
            _context.Importers.Add(importer);
            _context.SaveChanges();
            return importer;
        }

        public IEnumerable<Importer> GetAllImporters()
        {
            return _context.Importers;
        }

        public Importer GetImporterByName(string name)
        {
            Importer? importer = _context.Importers.FirstOrDefault(i => i.Name == name);

            if (importer == null)
            {
                throw new ArgumentException("Importer not found");
            }

            return importer;
        }
    }
}
