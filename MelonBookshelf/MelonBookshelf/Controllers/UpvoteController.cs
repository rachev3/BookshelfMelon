using AutoMapper;
using Humanizer.Localisation;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelf.Controllers
{
    public class UpvoteController : Controller
    {
        private readonly IUpvoteService upvoteService;
        private readonly IMapper mapper;
        public UpvoteController(IUpvoteService upvoteService, IMapper mapper)
        {
            this.upvoteService = upvoteService;
            this.mapper = mapper;   
        }

        public async Task<IActionResult> Index()
        {
            var data = await upvoteService.GetAll();
            var upvote = data.Select(x => new UpvoteViewModel(x)).ToList();
            var viewModel = new UpvotePageViewModel(upvote);
            return View("Upvote", viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UpvoteViewModel upvote)
        {
            var dto = mapper.Map<Upvote>(upvote);
            await upvoteService.Add(dto);
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
        public async Task<IActionResult> Edit(int id, UpvoteViewModel upvote)
        {
            var dto = mapper.Map<Upvote>(upvote);
            upvote.UpvoteId = id;
            if (!ModelState.IsValid)
            {
                return View(upvote);
            }
            await upvoteService.Update(id, dto);
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
