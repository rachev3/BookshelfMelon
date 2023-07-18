namespace MelonBookshelf.Models
{
    public class RequestPageViewModel
    {
        public RequestPageViewModel(List<RequestViewModel> requests, List<CategoryViewModel> categories)
        {
            Requests = requests;
            Categories = categories;
        }
        public List<RequestViewModel> Requests = new();
        public List<CategoryViewModel> Categories = new();
    }
}
