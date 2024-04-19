using Domain;

namespace WebModels.BuildingModels
{
    public class UpdateFlatRequestModel
    {
        public int Floor { get; set; }
        public int Number { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string OwnerEmail { get; set; }
        public int Bathrooms { get; set; }
        public bool HasBalcony { get; set; }

        public Flat ToEntity()
        {
            return new Flat
            {
                Floor = Floor,
                Number = Number,
                OwnerName = OwnerName,
                OwnerSurname = OwnerSurname,
                OwnerEmail = OwnerEmail,
                Bathrooms = Bathrooms,
                HasBalcony = HasBalcony
            };
        }
    }
}