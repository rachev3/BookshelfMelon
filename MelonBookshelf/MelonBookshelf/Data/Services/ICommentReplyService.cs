using MelonBookshelf.Data.DTO;

namespace MelonBookshelf.Data.Services
{
    public interface ICommentReplyService
    {
        Task<List<CommentReply>> GetAll();
        Task<CommentReply> GetById(int id);
        Task Add(CommentReply category);
        Task Delete(int id);
    }
}
