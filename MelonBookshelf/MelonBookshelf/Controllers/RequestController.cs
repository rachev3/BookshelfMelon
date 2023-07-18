using Microsoft.AspNetCore.Mvc;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using System.Linq;
using System.Security.Claims;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using MelonBookshelf.Data;

namespace MelonBookshelf.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;
        private readonly ICategoryService categoryService;
        private readonly Data.Services.IResourceService resourceService;
        private readonly IUserService userService;
        public RequestController(IRequestService requestService, ICategoryService categoryService, IUserService userService, Data.Services.IResourceService resourceService)
        {
            this.requestService = requestService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.resourceService = resourceService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await requestService.GetAll();
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var requests = data.Select(x => new RequestViewModel(x)).ToList();
            var viewModel = new RequestPageViewModel(requests,viewListCategory);
            return View("Request", viewModel);
        }
        public async Task<IActionResult> MyRequests()
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var data = await requestService.GetMyRequests(userId);
            var requests = data.Select(x => new RequestViewModel(x)).ToList();
            var viewModel = new RequestPageViewModel(requests, viewListCategory);
            return View("MyRequests", viewModel);
        }
        public async Task<IActionResult> FollowingRequests()
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var data = await requestService.GetFollowingRequests(userId);
            var requests = data.Select(x => new RequestViewModel(x)).ToList();
            var viewModel = new RequestPageViewModel(requests, viewListCategory);
            return View("FollowingRequests", viewModel);
        }
        public async Task<IActionResult> Following()
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var data = await requestService.GetMyRequests(userId);
            var requests = data.Select(x => new RequestViewModel(x)).ToList();
            var viewModel = new RequestPageViewModel(requests, viewListCategory);
            return View("MyRequests", viewModel);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PendingRequests()
        {
            var data = await requestService.GetPendingRequests();
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var requests = data.Select(x => new RequestViewModel(x)).ToList();
            var viewModel = new RequestPageViewModel(requests, viewListCategory);
            return View("PendingRequests", viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await requestService.GetById(id);
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            RequestEditViewModel request = new(data,viewListCategory);
            return View("Details", request);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var model = new RequestEditViewModel(viewListCategory);
            return View("Create",model);
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(Request request)
        {
            string name = User.Identity.Name;
            var user = userService.GetByName(name);
            request.User = user.Result;
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
            var viewModel = new RequestEditViewModel(request, viewListCategory);
            if (request == null)
            {
                return View("NotFound");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Request request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            //var check = requestService.GetById(id);
            //var item = check.Result;
            //if(item.Status != RequestStatus.PendingConfirmation)
            //{
            //    return View("NotFound");
            //}
            await requestService.Update(request);
            var result = requestService.GetById(request.RequestId);
            var resultRequest = result.Result;

            if(resultRequest.Status == RequestStatus.Delivered)
            {
                var resource = new Resource();
                resource.Status = ResourceStatus.Available;
                resource.Title = result.Result.Title;
                resource.Description = result.Result.Description;
                resource.Location = "Bookshelf";
                resource.Category = result.Result.Category;
                resource.Author = result.Result.Author;
                resource.DateAdded = DateTime.UtcNow;
                resource.Type = ResourceType.Physical;

                await resourceService.Add(resource);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> EditPendingRequest(int id)
        {
            var request = await requestService.GetById(id);
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var viewModel = new RequestEditViewModel(request, viewListCategory);
            if (request == null)
            {
                return View("NotFound");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPendingRequest(Request request)
        {
            //request.RequestId = id;
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            
            await requestService.Update(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var request = await requestService.GetById(id);
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            RequestEditViewModel viewModel = new(request, viewListCategory);
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
