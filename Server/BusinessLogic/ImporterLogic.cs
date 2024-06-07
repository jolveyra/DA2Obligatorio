using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using RepositoryInterfaces;
using LogicInterfaces;
using CustomExceptions;

namespace BusinessLogic
{
    public class ImporterLogic
    {
        private IImporterRepository _iImporterRepository;

        public ImporterLogic(IImporterRepository iImporterRepository)
        {
            _iImporterRepository = iImporterRepository;
        }

        public Importer CreateImporter(Importer importer)
        {
            CheckUniqueImporterName(importer);

            return _iImporterRepository.CreateImporter(importer);
        }

        private void CheckUniqueImporterName(Importer importer)
        {
            if (_iImporterRepository.GetAllImporters().ToList().Exists(x => x.Name.Equals(importer.Name)))
            {
                throw new ImporterException("Importer with same name already exists");
            }
        }
    }
}
