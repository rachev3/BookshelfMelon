namespace MelonBookshelf.Models
{
    public class FollowerViewModel
    {
        public FollowerViewModel()
        {

        }
        public FollowerViewModel(Follower follower)
        {
            RequestId = follower.RequestId;
            UserId = follower.UserId;
        }
        public int RequestId { get; set; }
        public string UserId { get; set; }
    }
}
