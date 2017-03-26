using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoDoctor
    {
        public int Id { get; set; }
        public string Image { get; set; }
        //public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Experience { get; set; }
        public string Manipulation { get; set; }
        public DtoAddress Address { get; set; }
        public DtoDoctorSpecialization Specialization { get; set; }
        public string Category { get; set; }
        public List<int> ClinicsIds { get; set; } //сделано как List<int> для облегчения добавления клиники к доктору на фронте
        //public List<int> ReviewsIds { get; set; } //сделано как List<int> для облегчения добавления клиники к доктору на фронте
        public double RateProfessionalism { get; set; }
        public double RateWaitingTime { get; set; }
        public double RatePoliteness { get; set; }
        public double RateAverage { get; set; }
        public int ReviewCount { get; set; }
        public bool Favorite { get; set; }
    }
}
