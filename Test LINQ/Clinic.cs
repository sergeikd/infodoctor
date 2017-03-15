using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_LINQ
{
    public class Clinic
    {
        public int Id { get; set; }
        public ICollection<ClinicSpecialization> ClinicSpecializations { get; set; }
    }
}
