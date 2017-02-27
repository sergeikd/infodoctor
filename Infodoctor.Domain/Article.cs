using System;

namespace Infodoctor.Domain
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public int ThemeId { get; set; }
        public virtual ArticleTheme Theme { get; set; }
    }
}
