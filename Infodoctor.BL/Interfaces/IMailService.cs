using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
   public interface IMailService
   {
       void Send(DtoMailMessage mail, DtoMailServiceConfiguration conf);
   }
}
