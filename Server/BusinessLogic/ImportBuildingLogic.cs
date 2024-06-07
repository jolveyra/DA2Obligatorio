using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingImporter;

namespace BusinessLogic
{
    public class ImportBuildingLogic
    {
        private IBuildingImporter _buildingImporter;

        public ImportBuildingLogic(IBuildingImporter buildingImporter)
        {
            _buildingImporter = buildingImporter;
        }



    }
}
