using Microsoft.AspNetCore.Mvc;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using System.Linq;
using System.Security.Claims;
using System.ComponentModel.Design;

namespace MelonBookshelf.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;
        private readonly ICategoryService categoryService;
        public RequestController(IRequestService requestService, ICategoryService categoryService)
        {
            this.requestService = requestService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await requestService.GetAll();
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var requests = data.Select(x => new RequestViewModel(x,viewListCategory)).ToList();
            var viewModel = new RequestPageViewModel(requests);
            return View("Request", viewModel);
        }
        public async Task<IActionResult> MyRequests()
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var data = await requestService.GetMyRequests(userId);
            var requests = data.Select(x => new RequestViewModel(x,viewListCategory)).ToList();
            var viewModel = new RequestPageViewModel(requests);
            return View("MyRequests", viewModel);
        }
        public async Task<IActionResult> PendingRequests()
        {
            var data = await requestService.GetPendingRequests();
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var requests = data.Select(x => new RequestViewModel(x,viewListCategory)).ToList();
            var viewModel = new RequestPageViewModel(requests);
            return View("PendingRequests", viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await requestService.GetById(id);
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            RequestViewModel request = new(data,viewListCategory);
            return View("Details", request);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var model = new RequestViewModel(viewListCategory);
            return View("Create",model);
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
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var viewModel = new RequestViewModel(request, viewListCategory);
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
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            RequestViewModel viewModel = new(request, viewListCategory);
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
