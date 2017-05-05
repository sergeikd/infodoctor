using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IResortService
    {
        IEnumerable<DtoResort> GetAllResorts(string pathToImage);
        DtoPagedResorts GetPagedResorts(int perPage, int numPage, string pathToImage);
        DtoPagedResorts SearchResorts(int perPage, int numPage, DtoResortSearchModel searchModel, string pathToImage);
        DtoResort GetResortById(int id, string pathToImage);
        void Add(DtoResort res);
        void Update(DtoResort res);
        void Delete(int id);
    }
}
