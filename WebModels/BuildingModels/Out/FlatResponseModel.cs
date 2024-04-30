using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace WebModels.BuildingModels
{
    public class FlatResponseModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string OwnerEmail { get; set; }
        public int Bathrooms { get; set; }
        public bool HasBalcony { get; set; }

        public FlatResponseModel(Flat flat)
        {
            Id = flat.Id;
            Number = flat.Number;
            Floor = flat.Floor;
            OwnerName = flat.Owner.Name;
            OwnerSurname = flat.Owner.Surname;
            OwnerEmail = flat.Owner.Email;
            Bathrooms = flat.Bathrooms;
            HasBalcony = flat.HasBalcony;
        }


        public override bool Equals(object? obj)
        {
            return obj is FlatResponseModel flatResponseModel && Id == flatResponseModel.Id;                
        }
    }
}
