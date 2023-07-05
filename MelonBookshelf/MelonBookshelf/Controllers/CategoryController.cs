using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelf.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await categoryService.GetAll();
            return View("Category", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await categoryService.Add(category);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryService.GetById(id);
            if (category == null)
            {
                return View("NotFound");
            }
            return View(category);
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
            if (category == null)
            {
                return View("NotFound");
            }
            return View(category);
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
