using AutoMapper;
using MelonBookshelf.Data;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using MelonBookshelf.Data.DTO;

namespace MelonBookshelf.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService resourceService;
        private readonly ICategoryService categoryService;
        private readonly IResourceCommentService commentService;
        private readonly IMapper mapper;
        private readonly IConfiguration _config;
        private readonly IUserService userService;
        public ResourceController (IResourceService resourceService, ICategoryService categoryService, IConfiguration config, IUserService userService, IResourceCommentService commentService)
        {
            this.resourceService = resourceService;
            this.categoryService = categoryService;
            this._config = config;
            this.userService = userService;
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            string userId = userService.GetByUserName(name).Result.Id;

            var resources = await resourceService.GetAll();
            var viewListResource = new List<ResourceViewModel>();

            foreach(var resource in resources)
            {
                var viewModel = new ResourceViewModel(resource, "ResourcesTable");
                var want = resource.WantedResources.FirstOrDefault(w => w.UserId == userId);
                if(want != null)
                {
                    viewModel.Want = true;
                }
                else
                {
                    viewModel.Want = false;
                }
                viewListResource.Add(viewModel);
            }

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var viewPageModel = new ResourcePageViewModel(viewListResource,viewListCategory);

            return View("Resource", viewPageModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? title, ResourceType? resourceType, int? categoryId)
        {
            var data = await resourceService.Search(title,resourceType,categoryId);

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c=> new CategoryViewModel(c)).ToList();

            var resources = data.Select(x => new ResourceViewModel(x)).ToList();
            var viewModel = new ResourcePageViewModel(resources,viewListCategory);

            return View("Resource", viewModel);
        }
        [HttpGet] 
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            ResourceEditViewModel resource = new(viewListCategory);
            return View(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResourceEditViewModel resource)
        {
            Category category = null;
            if (resource.CategoryId != null)
            {


                category = await categoryService.GetById(resource.CategoryId.Value);
            }
            Resource dto = new Resource(
                resource.Type,
                resource.Author,
                resource.Title,
                resource.Description,
                resource.Location,
                resource.Price,
                resource.Invoice,
                resource.Status,
                resource.DateAdded,
                resource.DateTaken,
                resource.DateReturn,
                null,
                null,
                null,
                null,
                resource.CategoryId, null);
            await resourceService.Add(dto);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await resourceService.GetById(id);

          
            ResourceViewModel resource =  new(data, "Details");
            return View("Details", resource);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string commingViewName)
        {
            var resource = await resourceService.GetById(id);

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var viewModel = new ResourceEditViewModel(resource,viewListCategory);
            if (resource == null)
            {
                return View("NotFound");
            }
            if(commingViewName == "Details")
            {
                return View("Edit",viewModel);
            }
            return PartialView("_Edit",viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ResourceEditViewModel resource)
        {
            Category category = null;
            if (resource.CategoryId != null)
            {


                category = await categoryService.GetById(resource.CategoryId.Value);
            }

            Resource dto = new Resource(
                
               resource.Type,
               resource.Author,
               resource.Title,
               resource.Description,
               resource.Location,
               resource.Price,
               resource.Invoice,
               resource.Status,
               resource.DateAdded,
               resource.DateTaken,
               resource.DateReturn,
               null,
               null,
               null,
               null,
               resource.CategoryId, null);
            dto.Category = category;
            dto.ResourceId = resource.ResourceId;
            if (!ModelState.IsValid)
            {
                return View(resource);
            }

            await resourceService.Update(resource.ResourceId, dto);

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList(); 

            var resources = await resourceService.GetAll();
            var viewListResource =  resources.Select(r=> new ResourceViewModel(r)).ToList();

            var viewModel = new ResourcePageViewModel(viewListResource, viewListCategory);
            return PartialView("_ResourceTable", viewModel);
        }
        public async Task<IActionResult> Want(int resourceId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await resourceService.Want(userId, resourceId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Unwant(int resourceId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await resourceService.Unwant(userId, resourceId);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(ResourceCommentViewModel resourceCommentViewModel, int resourceId)
        {
            string name = User.Identity.Name;
            User user = userService.GetByUserName(name).Result;

            ResourceComment resourceComment = new();
            resourceComment.Comment = resourceCommentViewModel.Comment;
            resourceComment.User = user;
            resourceComment.ResourceId = resourceId;

            await commentService.Add(resourceComment);


            var data = await resourceService.GetById(resourceId);


            ResourceViewModel resource = new(data, "Details");
            return View("Details", resource);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upload(IFormFile file, int resourceId)
        {
            Resource resource = null;
            resource = resourceService.GetById(resourceId).Result;
            Guid guid = Guid.NewGuid();
            string ext = Path.GetExtension(file.FileName);
            string shortLocation = _config.GetValue<string>("FileStorage:Path").ToString() + guid + ext;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), shortLocation);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
         
            resource.Location = shortLocation;
            resource.FileName = file.FileName;
            resourceService.Update(resource.ResourceId,resource);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
    
        public async Task<IActionResult> Download(int resourceId)
        {
            Resource resource = await resourceService.GetById(resourceId);
            if (resource == null)
            {
                
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), resource.Location);
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

        

            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, resource.FileName);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
           
            var resource = await resourceService.GetById(id);
            ResourceViewModel resourceViewModel = new(resource);
            if (resource == null)
            {
                return View("NotFound");
            }
            return View(resourceViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var resource = await resourceService.GetById(id);
            if (resource == null)
            {
                return View("NotFound");
            }

            await resourceService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
