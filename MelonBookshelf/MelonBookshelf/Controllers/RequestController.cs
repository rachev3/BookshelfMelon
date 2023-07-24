using Microsoft.AspNetCore.Mvc;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using System.Linq;
using System.Security.Claims;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using MelonBookshelf.Data;
using AutoMapper;
using Humanizer.Localisation;
using MelonBookshelf.Models.Email;

namespace MelonBookshelf.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService requestService;
        private readonly ICategoryService categoryService;
        private readonly Data.Services.IResourceService resourceService;
        private readonly IUserService userService;
        private readonly IEmailSender emailSender;

        public RequestController(IRequestService requestService, ICategoryService categoryService, IUserService userService, Data.Services.IResourceService resourceService, IEmailSender emailSender)
        {
            this.requestService = requestService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.resourceService = resourceService;
            this.emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await requestService.GetAll();
            var viewListRequest = requests.Select(x => new RequestViewModel(x)).ToList();

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var pageViewModel = new RequestPageViewModel(viewListRequest, viewListCategory);

            return View("Request", pageViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var requests = await requestService.GetMyRequests(userId);
            var viewListRequest = requests.Select(x => new RequestViewModel(x)).ToList();

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var pageViewModel = new RequestPageViewModel(viewListRequest, viewListCategory);

            return View("MyRequests", pageViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> FollowingRequests()
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var requests = await requestService.GetFollowingRequests(userId);
            var viewListRequest = requests.Select(x => new RequestViewModel(x)).ToList();

            var pageViewModel = new RequestPageViewModel(viewListRequest, viewListCategory);

            return View("FollowingRequests", pageViewModel);
        }  

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PendingRequests()
        {
            var requests = await requestService.GetPendingRequests();
            var viewListRequest = requests.Select(x => new RequestViewModel(x)).ToList();

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var pageViewModel = new RequestPageViewModel(viewListRequest, viewListCategory);

            return View("PendingRequests", pageViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var request = await requestService.GetById(id);

            RequestEditViewModel requestViewModel = new(request);

            return View("Details", requestViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var requestViewModel = new RequestEditViewModel(viewListCategory);

            return View("Create", requestViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestEditViewModel request)
        {
            string name = User.Identity.Name;
            User user = userService.GetByName(name).Result;

            Request dto = new Request(
                RequestStatus.PendingConfirmation,
                request.Type,
                request.Author,
                request.Title,
                request.Priority,
                request.Link,
                request.Description,
                request.Motive,
                request.DateAdded,
                null,
                user,
                request.Upvotes,
                request.Followers,
                request.CategoryId,
                request.Category
                );

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

        [HttpGet]
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
        public async Task<IActionResult> Edit(RequestEditViewModel request)
        {          
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var emails = await requestService.GetFollowersEmails(request.RequestId);

            var originalReq = await requestService.GetById(request.RequestId);
            var oldStatus = originalReq.Status;

            if (oldStatus != request.Status)
            {
                var message = new Message(emails, "Status Changed", "Request`s status is changed.");
                emailSender.SendEmail(message);
            }

            originalReq.Status = request.Status;
            originalReq.Type = request.Type;
            originalReq.Author = request.Author;
            originalReq.Title = request.Title;
            originalReq.Priority = request.Priority;
            originalReq.Link = request.Link;
            originalReq.Description = request.Description;
            originalReq.Motive = request.Motive;
            originalReq.DateAdded = request.DateAdded;
            originalReq.User = request.User;
            originalReq.CategoryId = request.CategoryId;
            originalReq.Category = request.Category;
           
            await requestService.Update(originalReq);

            //creating resource
            if (originalReq.Status == RequestStatus.Delivered)
            {
                var resource = new Resource();
                resource.Type = ResourceType.Physical;
                resource.Author = originalReq.Author;
                resource.Title = originalReq.Title;
                resource.Description = originalReq.Description;
                resource.Location = "Bookshelf";
                resource.Status = ResourceStatus.Available;
                resource.Category = originalReq.Category;
                resource.DateAdded = DateTime.UtcNow;

                await resourceService.Add(resource);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPendingRequest(int id)
        {
            var request = await requestService.GetById(id);

            if (request == null)
            {
                return View("NotFound");
            }

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var viewModel = new RequestEditViewModel(request, viewListCategory);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPendingRequest(RequestEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            Request dto = new Request(
                request.Status,
                request.Type,
                request.Author,
                request.Title,
                request.Priority,
                request.Link,
                request.Description,
                request.Motive,
                request.DateAdded,
                null,
                request.User,
                request.Upvotes,
                request.Followers,
                request.CategoryId,
                request.Category
                );
            dto.RequestId = request.RequestId;

            await requestService.Update(dto);

            var result = requestService.GetById(request.RequestId).Result;


            if (result.Status == RequestStatus.Delivered)
            {
                var resource = new Resource();
                resource.Type = ResourceType.Physical;
                resource.Author = result.Author;
                resource.Title = result.Title;
                resource.Description = result.Description;
                resource.Location = "Bookshelf";
                resource.Status = ResourceStatus.Available;
                resource.Category = result.Category;
                resource.DateAdded = DateTime.UtcNow;

                await resourceService.Add(resource);
            }
            return RedirectToAction(nameof(PendingRequests));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await requestService.GetById(id);

            if (request == null)
            {
                return View("NotFound");
            }

            //var categories = await categoryService.GetAll();
            //var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            RequestViewModel viewModel = new(request);

            return PartialView("_ConfirmDeleteMyRequest",viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int requestId)
        {
            var request = await requestService.GetById(requestId);

            if (request == null)
            {
                return View("NotFound");
            }

            await requestService.Delete(requestId);

            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var categories = await categoryService.GetAll();
            var categoriesViewModel = categories.Select(x => new CategoryViewModel(x)).ToList();

            var requests = await requestService.GetMyRequests(userId);
            var requestsViewModel = requests.Select(x => new RequestViewModel(x)).ToList();

            var viewModel = new RequestPageViewModel(requestsViewModel, categoriesViewModel);

            return PartialView("_MyRequestsTable", viewModel);
        }
    }
}
