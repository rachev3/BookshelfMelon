using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelf.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService resourceService;
        public ResourceController (IResourceService resourceService)
        {
            this.resourceService = resourceService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await resourceService.GetAll();
            return View("Resource", data);
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

        public async Task<IActionResult> Delete(int id)
        {
            var resource = await resourceService.GetById(id);
            if (resource == null)
            {
                return View("NotFound");
            }
            return View(resource);
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
