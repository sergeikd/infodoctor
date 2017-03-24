using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoDoctorCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Doctors { get; set; }
    }
}
