using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoDoctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Experience { get; set; }
        public string Manipulation { get; set; }
        public DtoAddress Address { get; set; }
        public string Specialization { get; set; }
        public string Category { get; set; }
        public List<int> ClinicsId { get; set; } //сделано как List<int> для облегчения добавления клиники к доктору на фронте
        public List<int> ReviewsId { get; set; } //сделано как List<int> для облегчения добавления клиники к доктору на фронте
        public double RatePrice { get; set; }
        public double RateQuality { get; set; }
        public double RatePoliteness { get; set; }
    }
}
