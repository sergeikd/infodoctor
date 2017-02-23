using Infodoctor.Domain;
using System.Collections.Generic;

namespace Infodoctor.BL.Intefaces
{
    public interface IClinicProfilesService
    {
        IEnumerable<ClinicProfile> GetAllProfiles();
        ClinicProfile GetProfileById(int id);
    }
}
