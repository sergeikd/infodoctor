using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Intefaces
{
   public  interface IMailService
   {
       void Send(DtoMailMessage mail, DtoMailServiceConfiguration conf);
   }
}
