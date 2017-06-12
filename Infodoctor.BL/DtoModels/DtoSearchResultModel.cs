using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoSearchResultModel
    {
        public List<DtoClinicMultilang> Clinics { get; set; }
        public List<DtoClinicSpecializationMultilang> ClinicSpecializations { get; set; }
    }
}
