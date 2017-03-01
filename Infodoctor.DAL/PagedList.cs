using System;
using System.Collections.Generic;
using System.Linq;

namespace Infodoctor.DAL
{
    public class PagedList<T> : List<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedList(IQueryable<T> source, int perPage, int pageNum)
        {
            if (source == null) return;
            //pageNum = pageNum ?? 1;
            Page = pageNum < 1 ? 1 : pageNum;
            PageSize = perPage < 1 ? 1 : perPage; ;
            TotalCount = source.Count();
            AddRange(source.Skip((Page - 1) * PageSize).Take(PageSize));
        }
    }
}
