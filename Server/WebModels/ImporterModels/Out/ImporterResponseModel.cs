using Domain;

namespace WebModels.ImporterModels
{
    public class ImporterResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Path { get; set; }

        public ImporterResponseModel(Importer importer)
        {
            Id = importer.Id;
            Name = importer.Name;
            Path = importer.Path;
        }

        public override bool Equals(object? obj)
        {
            return obj is ImporterResponseModel importer && Id == importer.Id;
        }

    }
}