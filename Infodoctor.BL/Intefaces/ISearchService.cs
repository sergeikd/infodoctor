using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Intefaces
{
    public interface ISearchService
    {
        List<string> FastSearch(DtoFastSearchModel searchModel);
        void RefreshCache();
    }
}
