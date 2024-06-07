using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain;
using RepositoryInterfaces;
using LogicInterfaces;
using CustomExceptions;
using System.Xml.Linq;
using BuildingImporter;

namespace BusinessLogic
{
    public class ImporterLogic: IImporterLogic
    {
        private IImporterRepository _iImporterRepository;
        private string _importersPath = @".\Importers";

        public ImporterLogic(IImporterRepository iImporterRepository)
        {
            _iImporterRepository = iImporterRepository;
        }

        public Importer CreateImporter(Importer importer)
        {
            CheckUniqueImporterName(importer);

            return _iImporterRepository.CreateImporter(importer);
        }

        public IEnumerable<Importer> GetAllImporters()
        {
            return _iImporterRepository.GetAllImporters();
        }

        private void CheckUniqueImporterName(Importer importer)
        {
            if (string.IsNullOrEmpty(importer.Name))
            {
                throw new ImporterException("Importer name cannot be empty");
            }

            if (_iImporterRepository.GetAllImporters().ToList().Exists(x => x.Name.Equals(importer.Name)))
            {
                throw new ImporterException("Importer with same name already exists");
            }
        }
    }
}
