using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelf.Controllers
{
    public class FollowerController : Controller
    {
        private readonly IFollowerService followerService;
        public FollowerController(IFollowerService followerService)
        {
            this.followerService = followerService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await followerService.GetAll();
            return View("Follower", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Follower follower)
        {
            await followerService.Add(follower);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var follower = await followerService.GetById(id);
            if (follower == null)
            {
                return View("NotFound");
            }
            return View(follower);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Follower follower)
        {
            follower.Id = id;
            if (!ModelState.IsValid)
            {
                return View(follower);
            }
            await followerService.Update(id, follower);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var follower = await followerService.GetById(id);
            if (follower == null)
            {
                return View("NotFound");
            }
            return View(follower);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var follower = await followerService.GetById(id);
            if (follower == null)
            {
                return View("NotFound");
            }

            await followerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
