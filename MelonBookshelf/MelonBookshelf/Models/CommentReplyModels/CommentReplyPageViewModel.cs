namespace MelonBookshelf.Models.CommentReplyModels
{
    public class CommentReplyPageViewModel
    {
        public CommentReplyPageViewModel(List<CommentReplyViewModel> commentReplies)
        {
            CommentReplies = commentReplies;
        }

        public List<CommentReplyViewModel> CommentReplies { get; set; }
    }
}
