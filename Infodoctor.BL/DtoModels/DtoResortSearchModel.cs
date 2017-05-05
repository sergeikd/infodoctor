namespace Infodoctor.BL.DtoModels
{
    public class DtoResortSearchModel
    {
        public string SearchWord { get; set; }
        public int CityId { get; set; }
        public string SortBy { get; set; }
        public string Descending { get; set; }
    }
}
