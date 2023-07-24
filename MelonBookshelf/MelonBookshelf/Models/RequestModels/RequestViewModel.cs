using Humanizer.Localisation;
using MelonBookshelf.Data;
using MimeKit.Cryptography;

namespace MelonBookshelf.Models
{
    public class RequestViewModel
    { 
        public RequestViewModel()
        {

        }
        public RequestViewModel(Request request)
        {
            RequestId = request.RequestId;
            Status = request.Status;
            Type = request.Type;
            Author = request.Author;
            Title = request.Title;
            Priority = request.Priority;
            Link = request.Link;
            Motive = request.Motive;
            DateAdded = request.DateAdded;
            Description = request.Description;
            Upvotes = request.Upvotes;
            Followers = request.Followers;
            User = request.User;
            Category = request.Category;
            User = request.User;

        }

        public int RequestId { get; set; }
        public RequestStatus? Status { get; set; }
        public ResourceType? Type { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Priority { get; set; }
        public string? Link { get; set; }
        public string? Motive { get; set; }
        public string? Description { get; set; }
        public string? CommingViewName { get; set; }
        public bool Liked { get; set; }
        public bool Followed { get; set; }
        public DateTime? DateAdded { get; set; }
        public FollowerPageViewModel FollowerPageViewModel { get; set; }
        public List<Upvote>? Upvotes { get; set; } = new List<Upvote>();
        public List<Follower>? Followers { get; set; } = new List<Follower>();

        public Category Category { get; set; }
        public User? User { get; set; }

    }
}
