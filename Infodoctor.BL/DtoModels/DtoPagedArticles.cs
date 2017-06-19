using System;
using System.Collections.Generic;
using Infodoctor.Domain.Entities;

namespace Infodoctor.BL.DtoModels
{
    public class DtoPagedArticles
    {
        public List<DtoArticle> Articles { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);  // total pages 
    }
}
