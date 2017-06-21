using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    //todo: убрать остатки специализаций
    public class DoctorSpecialization
    {
        public int Id { get; set; }
        public virtual ICollection<LocalizedDoctorSpecialization> LocalizedDoctorSpecializations { get; set; }
        public virtual ClinicSpecialization ClinicSpecialization { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }

    public class LocalizedDoctorSpecialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Language Language { get; set; }
    }
}
