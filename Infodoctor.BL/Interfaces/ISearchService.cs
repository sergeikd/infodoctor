using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface ISearchService
    {
        List<string> FastSearch(DtoFastSearchModel searchModel);
        void RefreshCache();
    }
}
