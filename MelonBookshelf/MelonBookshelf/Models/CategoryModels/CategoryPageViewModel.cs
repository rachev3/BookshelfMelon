namespace MelonBookshelf.Models
{
    public class CategoryPageViewModel
    {
        public CategoryPageViewModel(List<CategoryViewModel> categories)
        {
            Categories = categories;
        }
        public List<CategoryViewModel> Categories = new();
    }
}
