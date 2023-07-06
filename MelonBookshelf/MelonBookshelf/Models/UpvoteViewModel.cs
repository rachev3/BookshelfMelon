namespace MelonBookshelf.Models
{
    public class UpvoteViewModel
    {
        public UpvoteViewModel()
        {
            
        }
        public UpvoteViewModel(Upvote upvote)
        {
            UpvoteId = upvote.UpvoteId;
            UserId = upvote.UserId;
            RequestId = upvote.RequestId;
        }
     
        public int UpvoteId { get; set; }

        public string UserId { get; set; }

        public int RequestId { get; set; }
    }

}
