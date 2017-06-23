using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infodoctor.Domain.Entities
{
    public class Resort
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedResort> Localized { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string ImageName { get; set; }
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public bool Favorite { get; set; }
        public virtual ResortAddress Address { get; set; }
        public virtual ICollection<ResortReview> Reviews { get; set; }
    }

    public class LocalizedResort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialisations { get; set; }
        public virtual Language Language { get; set; }
    }

    public class ResortAddress
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedResortAddress> Localized { get; set; }
        [Required] //for prvent error message like here http://stackoverflow.com/questions/28887156/unable-to-determine-the-principal-end-of-an-association-between-the-types
        public virtual Resort Resort { get; set; }
    }

    public class ResortPhone
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedResortPhone> Localized { get; set; }
        public virtual ResortAddress Address { get; set; }
    }

    public class LocalizedResortAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public virtual City City { get; set; }
        public string Street { get; set; }
        public virtual ICollection<ResortPhone> Phones { get; set; }
        public virtual Language Language { get; set; }
    }

    public class LocalizedResortPhone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public virtual Language Language { get; set; }
    }
}
