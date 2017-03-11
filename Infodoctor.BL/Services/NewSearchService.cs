using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Infodoctor.DAL.Interfaces;

namespace Infodoctor.BL.Services
{
    public class NewSearchService
    {
        private readonly IСlinicRepository _clinicRepository;
        private readonly IClinicSpecializationRepository _clinicSpecializationRepository;

        private static List<string> ClinicsCache { get; set; }
        private static List<string> _virtualSpecssCache { get; set; }

        public NewSearchService(IСlinicRepository clinicRepository,
            IClinicSpecializationRepository clinicSpecializationRepository)
        {
            if (clinicRepository == null)
                throw new ArgumentNullException(nameof(clinicRepository));
            if (clinicSpecializationRepository == null)
                throw new ArgumentNullException(nameof(clinicSpecializationRepository));
            _clinicRepository = clinicRepository;
            _clinicSpecializationRepository = clinicSpecializationRepository;
        }

        public void ClinicSearch(string query)
        {
            var list1 = _clinicRepository.GetAllСlinics().Where(x => x.Name.Contains(query) || x.ClinicSpecializations.Any( y => y.Name.Contains(query))).Select(x => new { x.Id, x.Name}).ToList();
            var list2 = _clinicSpecializationRepository.GetAllClinicSpecializations().Where(x => x.Name.Contains(query)).Select(x => new { x.Id, x.Name }).ToList();
        } 
    }
}
