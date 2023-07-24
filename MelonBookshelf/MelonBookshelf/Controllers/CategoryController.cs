using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

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
            var resources = data.Select(x => new CategoryViewModel(x)).ToList();
            var viewModel = new CategoryPageViewModel(resources);

            return View("Category", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoryViewModel category)
        {
            var dbo = new Category();
            dbo.Name = category.Name;

            await categoryService.Add(dbo);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, CategoryViewModel category)
        {
            //do i need id?
            category.CategoryId = id;

            var dto = await categoryService.GetById(id);
            dto.Name = category.Name;

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await categoryService.Update(id, dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryService.GetById(id);
            CategoryViewModel categoryViewModel = new(category);

            if (category == null)
            {
                return View("NotFound");
            }

            return PartialView("_ConfirmDelete", categoryViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirm(int categoryId)
        {
            var category = await categoryService.GetById(categoryId);

            if (category == null)
            {
                return View("NotFound");
            }

            await categoryService.Delete(categoryId);

            var data = await categoryService.GetAll();
            var resources = data.Select(x => new CategoryViewModel(x)).ToList();
            var viewModel = new CategoryPageViewModel(resources);

            return PartialView("_CategoryTable", viewModel);
        }
    }
}
