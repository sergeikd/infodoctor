using System.Collections.Generic;
using Infodoctor.BL.DtoModels;

namespace Infodoctor.BL.Interfaces
{
    public interface IArticleCommentsService
    {
        IEnumerable<DtoArticleComment> GetComments();
        IEnumerable<DtoArticleComment> GetCommentsByArticleId(int id);
        DtoPagedArticleComments GetPagedCommentsByArticleId(int id, int perPage, int numPage);
        DtoArticleComment GetCommentById(int id);
        void Add(DtoArticleComment comment);
        void Delete(int id);
    }
}
