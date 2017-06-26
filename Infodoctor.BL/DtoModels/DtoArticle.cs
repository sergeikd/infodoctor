using System;
using System.Collections.Generic;

namespace Infodoctor.BL.DtoModels
{
    public class DtoArticle
    {
        #region SingleLang
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public List<DtoArticleComment> Comments { get; set; }
        public string LangCode { get; set; }
        #endregion
    }
}
