using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Intefaces
{
    public interface ISearchService
    {
        DtoSearchResultModel FullSeacrh(DtoSearchModel searchModel);
        List<string> FastSearch(DtoSearchModel searchModel);
    }
}
