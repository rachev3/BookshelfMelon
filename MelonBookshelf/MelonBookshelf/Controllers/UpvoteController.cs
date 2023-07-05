using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelf.Controllers
{
    public class UpvoteController : Controller
    {
        private readonly IUpvoteService upvoteService;
        public UpvoteController(IUpvoteService upvoteService)
        {
            this.upvoteService = upvoteService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await upvoteService.GetAll();
            return View("Upvote", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Upvote upvote)
        {
            await upvoteService.Add(upvote);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var upvote = await upvoteService.GetById(id);
            if (upvote == null)
            {
                return View("NotFound");
            }
            return View(upvote);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Upvote upvote)
        {
            upvote.UpvoteId = id;
            if (!ModelState.IsValid)
            {
                return View(upvote);
            }
            await upvoteService.Update(id, upvote);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var upvote = await upvoteService.GetById(id);
            if (upvote == null)
            {
                return View("NotFound");
            }
            return View(upvote);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var upvote = await upvoteService.GetById(id);
            if (upvote == null)
            {
                return View("NotFound");
            }

            await upvoteService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
