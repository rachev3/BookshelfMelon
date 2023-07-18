using AutoMapper;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Linq;

namespace MelonBookshelf.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await categoryService.GetAll();
            var resources = data.Select(x => new CategoryViewModel(x)).ToList();
            var viewModel = new CategoryPageViewModel(resources);

            return View("Category", viewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await categoryService.GetById(id);
            CategoryViewModel resource = new(data);
            return View("Details", resource);

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel category)
        {
            var dbo = mapper.Map<Category>(category);
            await categoryService.Add(dbo);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryService.GetById(id);
            CategoryViewModel categoryViewModel = new(category);
            if (category == null)
            {
                return View("NotFound");
            }
            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            category.CategoryId = id;
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await categoryService.Update(id, category);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryService.GetById(id);
            CategoryViewModel categoryViewModel = new(category);
            if (category == null)
            {
                return View("NotFound");
            }
            return View(categoryViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var category = await categoryService.GetById(id);
            if (category == null)
            {
                return View("NotFound");
            }

            await categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
