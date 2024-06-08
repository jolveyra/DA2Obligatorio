using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.ImportBuildingModels
{
    public class ImportBuildingResponseModel
    {
        public string Message { get; set; }

        public ImportBuildingResponseModel(string message)
        {
            Message = message;
        }

        public override bool Equals(object obj)
        {
            return obj is ImportBuildingResponseModel model &&
                   Message == model.Message;
        }
    }
}
