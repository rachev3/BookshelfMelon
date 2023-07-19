using Microsoft.AspNetCore.Mvc;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using System.Linq;
using System.Security.Claims;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using MelonBookshelf.Data;
using AutoMapper;

namespace MelonBookshelf.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;
        private readonly ICategoryService categoryService;
        private readonly Data.Services.IResourceService resourceService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public RequestController(IRequestService requestService, ICategoryService categoryService, IUserService userService, Data.Services.IResourceService resourceService, IMapper mapper)
        {
            this.requestService = requestService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.resourceService = resourceService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await requestService.GetAll();
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var requests = data.Select(x => new RequestViewModel(x)).ToList();
            var viewModel = new RequestPageViewModel(requests, viewListCategory);
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
            RequestEditViewModel request = new(data, viewListCategory);
            return View("Details", request);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();
            var model = new RequestEditViewModel(viewListCategory);
            return View("Create", model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(RequestEditViewModel request)
        {
            //var category = new Category();
            //category.Name = request.Category.Name;
            //category.CategoryId = request.Category.CategoryId;
            Category category = null;
            if (request.CategoryId != null)
            {

                
                category = await categoryService.GetById(request.CategoryId.Value);
            }

            string name = User.Identity.Name;
            var user = userService.GetByName(name);

            var dto = new Request();
            dto.Title = request.Title;
            dto.Description = request.Description;
            dto.Author = request.Author;
            dto.Status = RequestStatus.PendingConfirmation;
            //dto.User = request.User;
            dto.Upvotes = request.Upvotes;
            dto.Followers = request.Followers;
            dto.Motive = request.Motive;
            //dto.Category.Name = request.Category.Name;
            dto.Priority = request.Priority;
            dto.DateAdded = request.DateAdded;
            dto.Link = request.Link;
            dto.Type = request.Type;
            dto.User = user.Result;
            dto.Category = category;




            await requestService.Add(dto);
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
<<<<<<< HEAD
        public async Task<IActionResult> Edit(int id, Request request)
        {
            request.RequestId = id;
=======
        public async Task<IActionResult> Edit(RequestEditViewModel request)
        {
            var dto = mapper.Map<Request>(request);
>>>>>>> ad801379b967135e6b22bf9574195d21d99d723e
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
            await requestService.Update(dto);
            var result = requestService.GetById(request.RequestId);
            var resultRequest = result.Result;

            if (resultRequest.Status == RequestStatus.Delivered)
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
        public async Task<IActionResult> EditPendingRequest(RequestEditViewModel request)
        {
            var dto = mapper.Map<Request>(request);
            //request.RequestId = id;
            if (!ModelState.IsValid)
            {
                return View(request);
            }
<<<<<<< HEAD
            await requestService.Update(request);
            return RedirectToAction(nameof(Index));
=======

            await requestService.Update(dto);
            return RedirectToAction(nameof(PendingRequests));
>>>>>>> ad801379b967135e6b22bf9574195d21d99d723e
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
