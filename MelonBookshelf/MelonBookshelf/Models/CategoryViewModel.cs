namespace MelonBookshelf.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {

        }
        public CategoryViewModel(Category category)
        {
            CategoryId = category.CategoryId;
            Name = category.Name;
        }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
    }
}
