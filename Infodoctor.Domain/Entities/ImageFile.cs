using System.ComponentModel.DataAnnotations.Schema;

namespace Infodoctor.Domain.Entities
{
    public class ImageFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[ForeignKey("Clinic")]
        //public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }
        //public string Path { get; set; }
    }
}
