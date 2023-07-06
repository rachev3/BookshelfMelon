using MelonBookshelf.Data;

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
            CategoryId = request.CategoryId;
        }

        public int RequestId { get; set; }
        public RequestStatus? Status { get; set; }
        public ResourceType? Type { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Priority { get; set; }
        public string? Link { get; set; }
        public string? Motive { get; set; }
        public DateTime? DateAdded { get; set; }


        public int? CategoryId { get; set; }

    }
}
