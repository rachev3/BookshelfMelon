using AutoMapper;
using MelonBookshelf.Data.DTO;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using IResourceService = MelonBookshelf.Data.Services.IResourceService;

namespace MelonBookshelf.Controllers
{
    public class ResourceCommentController : Controller
    {
        private readonly IResourceCommentService resourceCommentService;
        private readonly IResourceService resourceService;
        private readonly IUserService userService;
        public ResourceCommentController(IResourceCommentService resourceCommentService, IUserService userService, IResourceService resourceService)
        {
            this.resourceCommentService = resourceCommentService;
            this.userService = userService;
            this.resourceService = resourceService;
        }

        public async Task<IActionResult> Index()
        {
            var resourceComments = await resourceCommentService.GetAll();
            var viewListResourceComments = resourceComments.Select(x => new ResourceCommentViewModel(x)).ToList();
            var viewPageModel = new ResourceCommentPageViewModel(viewListResourceComments);

            return View("", viewPageModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResourceCommentViewModel resourceCommentViewModel, int resourceId)
        {
            string name = User.Identity.Name;
            User user = userService.GetByUserName(name).Result;

            ResourceComment resourceComment = new();
            resourceComment.Comment = resourceCommentViewModel.Comment;
            resourceComment.User = user;
            resourceComment.ResourceId = resourceId;

            await resourceCommentService.Add(resourceComment);


            var data = await resourceService.GetById(resourceId);


            ResourceViewModel resource = new(data, "Details");
            return View("Details", resource);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var resourceComment = await resourceCommentService.GetById(id);
            if (resourceComment == null)
            {
                return View("NotFound");
            }

            await resourceCommentService.Delete(id);
            return View();
        }
    }
}
