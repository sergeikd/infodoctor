
using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoClinicSpecialization
    {
        public int Id { get; set; }
        public List<LocalizedDtoClinicSpecialization> LocalizedDtoClinicSpecializations { get; set; }
    }

    public class LocalizedDtoClinicSpecialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }
    }
}
