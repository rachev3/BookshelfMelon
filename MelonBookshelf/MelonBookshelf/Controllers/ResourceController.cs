using AutoMapper;
using MelonBookshelf.Data;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace MelonBookshelf.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService resourceService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        public ResourceController (IResourceService resourceService, ICategoryService categoryService)
        {
            this.resourceService = resourceService;
            this.categoryService = categoryService;
        
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await resourceService.GetAll();

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var resources = data.Select(x => new ResourceViewModel(x)).ToList();
            var viewModel = new ResourcePageViewModel(resources,viewListCategory);

            return View("Resource", viewModel);
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
                resource.CategoryId, null);
            await resourceService.Add(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await resourceService.GetById(id);

            //var categories = await categoryService.GetAll();
            //var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            ResourceViewModel resource =  new(data);
            return View("Details", resource);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var resource = await resourceService.GetById(id);

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var viewModel = new ResourceEditViewModel(resource,viewListCategory);
            if (resource == null)
            {
                return View("NotFound");
            }
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ResourceEditViewModel resource)
        {
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

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
               resource.CategoryId, null);
            dto.Category = category;
            dto.ResourceId = resource.ResourceId;
            if (!ModelState.IsValid)
            {
                return View(resource);
            }
            await resourceService.Update(resource.ResourceId, dto);
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Download()
        {
            byte[] bytes = Encoding.UTF8.GetBytes("My first file");
            return File(bytes, "text/plain", "file.txt");
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var categories = await categoryService.GetAll();
            //var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

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
