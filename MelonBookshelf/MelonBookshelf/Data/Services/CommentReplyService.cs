using MelonBookshelf.Data.DTO;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.X509;

namespace MelonBookshelf.Data.Services
{
    public class CommentReplyService : ICommentReplyService
    {
        private readonly ApplicationDbContext _appDbContext;
        public CommentReplyService(ApplicationDbContext applicationDbContext)
        {
            _appDbContext = applicationDbContext;   
        }
        public async Task<List<CommentReply>> GetAll()
        {
            var result = await _appDbContext.CommentReplys.Include(c => c.User).ToListAsync();
            await _appDbContext.SaveChangesAsync();
            return result;
        }
        public async Task<CommentReply> GetById(int id)
        {
            var result = await _appDbContext.CommentReplys.FirstOrDefaultAsync(rc => rc.ReplyId == id);
            await _appDbContext.SaveChangesAsync();
            return result;
        }
        public async Task Add(CommentReply reply)
        {
            _appDbContext.CommentReplys.Add(reply);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var result = await _appDbContext.CommentReplys.FirstOrDefaultAsync(n => n.ReplyId == id);
            _appDbContext.CommentReplys.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }


    }
}
