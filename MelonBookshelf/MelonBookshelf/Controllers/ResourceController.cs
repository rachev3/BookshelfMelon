using MelonBookshelf.Data;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace MelonBookshelf.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService resourceService;
        private readonly ICategoryService categoryService;
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
            var viewModel = new ResourcePageViewModel(resources, viewListCategory);

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
        
        public async Task<IActionResult> Details(int id)
        {
            var data = await resourceService.GetById(id);
            ResourceViewModel resource =  new(data);
            return View("Details", resource);

        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Resource resource)
        {
            await resourceService.Add(resource);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var resource = await resourceService.GetById(id);
            if (resource == null)
            {
                return View("NotFound");
            }
            return View(resource);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Resource resource)
        {
            resource.ResourceId = id;
            if (!ModelState.IsValid)
            {
                return View(resource);
            }
            await resourceService.Update(id, resource);
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
