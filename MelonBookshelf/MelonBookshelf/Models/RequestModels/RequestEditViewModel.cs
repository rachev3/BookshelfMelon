using MelonBookshelf.Data;

namespace MelonBookshelf.Models
{
    public class RequestEditViewModel
    {
        public RequestEditViewModel()
        {

        }
        public RequestEditViewModel(List<CategoryViewModel> categories)
        {
            Categories = categories;
        }
        public RequestEditViewModel(Request request)
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
            CategoryId = request.CategoryId;
            Category = request.Category;

        }
        public RequestEditViewModel(Request request, string commingViewName)
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
            CategoryId = request.CategoryId;
            Category = request.Category;
            CommingViewName = commingViewName;

        }
        public RequestEditViewModel(Request request, List<CategoryViewModel> categories, string commingViewName)
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
            Categories = categories;
            CategoryId = request.CategoryId;
            Category = request.Category;
            CommingViewName = commingViewName;
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
        public DateTime? DateAdded { get; set; }
        public FollowerPageViewModel? FollowerPageViewModel { get; set; }
        public List<Upvote>? Upvotes { get; set; } = new List<Upvote>();
        public List<Follower>? Followers { get; set; } = new List<Follower>();

        public int? CategoryId { get; set; } 
        public Category? Category {get;set;}
        public List<CategoryViewModel>? Categories { get; set; }

        public User? User { get; set; }

    }
}
