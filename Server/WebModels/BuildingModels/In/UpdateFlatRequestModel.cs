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
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public bool HasBalcony { get; set; }
        public bool ChangeOwner { get; set; }

        public Flat ToEntity()
        {
            return new Flat
            {
                Floor = Floor,
                Number = Number,
                Owner = new Person()
                {
                    Name = OwnerName,
                    Surname = OwnerSurname,
                    Email = OwnerEmail
                },
                Rooms = Rooms,
                Bathrooms = Bathrooms,
                HasBalcony = HasBalcony
            };
        }
    }
}