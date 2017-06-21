namespace Infodoctor.BL.DtoModels
{
    public class DtoClinicSearchModel
    {
        public string SearchWord { get; set; }
        public int CityId { get; set; }
        //public List<int> SpecializationIds { get; set; } todo: удалить остатки от специализации
        public string SortBy { get; set; }
        public string Descending { get; set; }
    }
}
