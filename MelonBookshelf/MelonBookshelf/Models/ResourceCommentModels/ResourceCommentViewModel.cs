using MelonBookshelf.Data.DTO;

namespace MelonBookshelf.Models
{
    public class ResourceCommentViewModel
    {
        public ResourceCommentViewModel()
        {

        }

        public ResourceCommentViewModel(ResourceComment resourceComment)
        {
            ResourceCommentId = resourceComment.CommentId;
            UserId = resourceComment.UserId;
            ResourceId = resourceComment.ResourceId;
            Comment = resourceComment.Comment;
            User = resourceComment.User;
        }

        public int ResourceCommentId { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
        public int ResourceId { get; set; }
        public string Comment { get; set; }
    }
}
