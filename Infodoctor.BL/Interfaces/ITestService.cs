using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Interfaces
{
    public interface ITestService
    {
        void Add100Clinics();
        void Add100Doctors();
        Clinic PrepareClinic();
    }
}
