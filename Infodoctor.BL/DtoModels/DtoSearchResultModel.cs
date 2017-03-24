using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoSearchResultModel
    {
        public List<DtoClinic> Clinics { get; set; }
        public List<DtoClinicSpecialization> ClinicSpecializations { get; set; }
    }
}
