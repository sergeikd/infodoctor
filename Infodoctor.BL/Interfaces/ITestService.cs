using System.Drawing;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.Interfaces
{
    public interface ITestService
    {
        void Add10Clinics(string pathToImage, Point[] imagesSizes);
        void AddClinicsFromOldDb();
        void Add10Doctors();
        
        Clinic PrepareClinic();
    }
}
