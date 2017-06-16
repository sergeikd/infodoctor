using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoDoctorSpecializationSilngleLang
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClinicSpecializationId { get; set; }
        public string LangCode { get; set; }
    }

    public class DtoDoctorSpecializationMultilagLang
    {
        public int Id { get; set; }
        public int ClinicSpecializationId { get; set; }
        public List<DtoDoctorSpecializationLocalized> Localized { get; set; }
    }

    public class DtoDoctorSpecializationLocalized
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }
    }
}
