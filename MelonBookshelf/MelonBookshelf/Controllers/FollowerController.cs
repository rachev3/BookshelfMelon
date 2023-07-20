using AutoMapper;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MelonBookshelf.Controllers
{
    public class FollowerController : Controller
    {
        private readonly IFollowerService followerService;
        private readonly IMapper mapper;
        public FollowerController(IFollowerService followerService, IMapper mapper)
        {
            this.followerService = followerService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await followerService.GetAll();
            var followers = data.Select(x => new FollowerViewModel(x)).ToList();
            var viewModel = new FollowerPageViewModel(followers);
            return View("Follower", viewModel);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FollowerViewModel follower)
        {
            var dto = mapper.Map<Follower>(follower);
            await followerService.Add(dto);
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
