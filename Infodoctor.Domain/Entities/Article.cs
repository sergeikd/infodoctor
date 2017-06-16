using System;
using System.Collections.Generic;

namespace Infodoctor.Domain.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public virtual ICollection<ArticleComment> Comments { get; set; }
    }

    public class ArticleTheme
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
