using MelonBookshelf.Data;
using MelonBookshelf.Data.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelonBookshelf.Models
{
    public class ResourceViewModel
    {
        public ResourceViewModel()
        {

        }
        public ResourceViewModel(Resource resource)
        {
            ResourceId = resource.ResourceId;
            Type = resource.Type;
            Author = resource.Author;
            Title = resource.Title;
            Description = resource.Description;
            Location = resource.Location;
            Price = resource.Price;
            Invoice = resource.Invoice;
            Status = resource.Status;
            DateAdded = resource.DateAdded;
            DateTaken = resource.DateTaken;
            DateReturn = resource.DateReturn;
            wantedResources = resource.WantedResources;
            Category = resource.Category;
            FileName = resource.FileName;
            ResourceCommentViewModel.ResourceId = resource.ResourceId;
        }
        public ResourceViewModel(Resource resource, string commingViewName)
        {
            ResourceId = resource.ResourceId;
            Type = resource.Type;
            Author = resource.Author;
            Title = resource.Title;
            Description = resource.Description;
            Location = resource.Location;
            Price = resource.Price;
            Invoice = resource.Invoice;
            Status = resource.Status;
            DateAdded = resource.DateAdded;
            DateTaken = resource.DateTaken;
            DateReturn = resource.DateReturn;
            wantedResources = resource.WantedResources;
            ResourceComments = resource.Comments;
            Category = resource.Category;
            FileName = resource.FileName;
            CommingViewName = commingViewName;
            ResourceCommentViewModel.ResourceId = resource.ResourceId;
            
        }

        public int ResourceId { get; set; }
        public ResourceType Type { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? FileName { get; set; }
        public double? Price { get; set; }
        public string? Invoice { get; set; }
        public bool Want { get; set; }
        public string CommingViewName { get; set; }
        public ResourceStatus? Status { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateTaken { get; set; }
        public DateTime? DateReturn { get; set; }
        public Category Category { get; set; }
        public List<WantedResources>? wantedResources { get; set; }
        public ResourceCommentViewModel ResourceCommentViewModel = new();
        public List<ResourceComment>? ResourceComments { get; set; }
        public CommentReplyViewModel CommentReplyViewModel = new();
    
    }
}
