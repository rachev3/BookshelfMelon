namespace MelonBookshelf.Models
{
    public class RequestPageViewModel
    {
        public RequestPageViewModel(List<RequestViewModel> requests)
        {
             Requests = requests;
        }
        public List<RequestViewModel> Requests = new();
    }
}
