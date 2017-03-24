namespace Infodoctor.BL.DtoModels
{
    public class DtoDoctorSearchModel
    {
        public string SearchWord { get; set; }
        public int CityId { get; set; }
        public int SpecializationId { get; set; }
        public string SortBy { get; set; }
        public string Descending { get; set; }
    }
}
