namespace Infodoctor.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; } //английское название языка
        public string Code { get; set; } //2-х символьный код языка по стандарту ISO 639-1
    }

}
