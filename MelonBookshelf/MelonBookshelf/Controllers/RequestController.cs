using Microsoft.AspNetCore.Mvc;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using System.Linq;
using System.Security.Claims;

namespace MelonBookshelf.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;
        public RequestController(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await requestService.GetAll();
            var requests = data.Select(x => new RequestViewModel(x)).ToList();
            var viewModel = new RequestPageViewModel(requests);
            return View("Request", viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await requestService.GetById(id);
            RequestViewModel request = new(data);
            return View("Details", request);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Request request)
        {
            await requestService.Add(request);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Upvote(int requestId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await requestService.Like(requestId, userId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Downvote(int requestId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await requestService.Dislike(requestId, userId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Follow(int requestId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await requestService.Follow(requestId, userId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Unfollow(int requestId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await requestService.UnFollow(requestId, userId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var request = await requestService.GetById(id);
            var viewModel = new RequestViewModel(request);
            if (request == null)
            {
                return View("NotFound");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Request request)
        {
            request.RequestId = id;
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            await requestService.Update(id, request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var request = await requestService.GetById(id);
            RequestViewModel viewModel = new(request);
            if (request == null)
            {
                return View("NotFound");
            }
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var request = await requestService.GetById(id);
            if (request == null)
            {
                return View("NotFound");
            }

            await requestService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
