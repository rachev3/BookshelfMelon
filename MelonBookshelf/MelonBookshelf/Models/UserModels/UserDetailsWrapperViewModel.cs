using MelonBookshelf.Models;

namespace MelonBookshelf.Models
{
    public class UserDetailsWrapperViewModel
    {
        public UserDetailsWrapperViewModel(UserViewModel user, List<RequestViewModel> followingRequests, List<RequestViewModel> myRequests, List<CategoryViewModel> categories)
        {
            User = user;
            Categories = categories;
            MyRequestsPageViewModel = new RequestPageViewModel(myRequests, categories);
            FollowingRequestsPageViewModel = new RequestPageViewModel(followingRequests, categories);

        }
        public UserViewModel User { get; set; }
        public UserChangePasswordViewModel UserChangePasswordViewModel = new();
        public List<CategoryViewModel> Categories { get; set; }
        public RequestPageViewModel MyRequestsPageViewModel { get; set; }
        public RequestPageViewModel FollowingRequestsPageViewModel { get; set; }
    }
}
