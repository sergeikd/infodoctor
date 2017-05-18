using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Interfaces
{
    public interface ITestService
    {
        void Add100Clinics(string pathToImage);
        void Add100Doctors();
        Clinic PrepareClinic();
    }
}
