using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MelonBookshelf.Controllers
{
    public class WantedResourcesController : Controller
    {
        private readonly IWantedResourcesService wantedResourcesService;
        public WantedResourcesController(IWantedResourcesService wantedResourcesService)
        {
            this.wantedResourcesService = wantedResourcesService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await wantedResourcesService.GetAll();
            var resources = data.Select(x => new WantedResourcesViewModel(x)).ToList();
            var viewModel = new WantedResourcesPageViewModel(resources);
            return View("WantedResources", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WantedResources wantedResources)
        {
            await wantedResourcesService.Add(wantedResources);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var wantedResources = await wantedResourcesService.GetById(id);
            if (wantedResources == null)
            {
                return View("NotFound");
            }
            return View(wantedResources);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, WantedResources wantedResources)
        {
            wantedResources.Id = id;
            if (!ModelState.IsValid)
            {
                return View(wantedResources);
            }
            await wantedResourcesService.Update(id, wantedResources);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var wanted = await wantedResourcesService.GetById(id);
            if (wanted == null)
            {
                return View("NotFound");
            }
            return View(wanted);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var wantedResources = await wantedResourcesService.GetById(id);
            if (wantedResources == null)
            {
                return View("NotFound");
            }

            await wantedResourcesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
