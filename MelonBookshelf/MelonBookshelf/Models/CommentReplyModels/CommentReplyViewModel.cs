using MelonBookshelf.Data.DTO;

namespace MelonBookshelf.Models
{
    public class CommentReplyViewModel
    {
        public CommentReplyViewModel()
        {
                
        }

        public CommentReplyViewModel(CommentReply commentReply)
        {
            ReplyId = commentReply.ReplyId;
            UserId = commentReply.UserId;
            User = commentReply.User;
            ResourceCommentId = commentReply.ResourceCommentId;
            Reply = commentReply.Reply;
        }

        public int ReplyId { get; set; } 
        public string UserId { get; set; }
        public User? User { get; set; }
        public int ResourceCommentId { get; set; }
        public string Reply {  get; set; }
    }
}
