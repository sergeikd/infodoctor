using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class Resort
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Specialisations { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public bool Favorite { get; set; }
        public virtual ResortAddress Address { get; set; }
        public virtual ICollection<ResortReview> Reviews { get; set; }
    }

    public class ResortAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public virtual City City { get; set; }
        public string Street { get; set; }
        public virtual ICollection<ResortPhone> Phones { get; set; }
        public virtual Resort Resort { get; set; }
    }

    public class ResortPhone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public virtual ResortAddress Address { get; set; }
    }
}
