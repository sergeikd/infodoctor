using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoClinicSpecialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DtoClinic> Clinic { get; set; }
    }
}
