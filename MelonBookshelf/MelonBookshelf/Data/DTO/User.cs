using MelonBookshelf.Data;
using MelonBookshelf.Data.DTO;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelonBookshelf.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        public List<Request> Requests { get; set; }
        public List<ResourceComment> Comments { get; set; }
    }
}
