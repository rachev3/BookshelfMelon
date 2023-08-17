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
            string name = User.Identity.Name;
            string userId = userService.GetByUserName(name).Result.Id;

            var requests = await requestService.GetAll();

            var viewListRequest = new List<RequestViewModel>();

            foreach (var request in requests)
            {
                var viewModel = new RequestViewModel(request);
                viewModel.CommingViewName = "RequestsTable";

                var upvote = request.Upvotes.FirstOrDefault(u => u.UserId == userId);
                var follow = request.Followers.FirstOrDefault(f => f.UserId == userId);

                if (upvote != null)
                {
                    viewModel.Liked = true;
                }

                if (follow != null)
                {
                    viewModel.Followed = true;
                }

                viewListRequest.Add(viewModel);
            }

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
            var viewListRequest = requests.Select(x => new RequestViewModel(x, "MyRequestsTable")).ToList();

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
            var viewListRequest = requests.Select(x => new RequestViewModel(x, "FollowingRequestsTable")).ToList();

            var pageViewModel = new RequestPageViewModel(viewListRequest, viewListCategory);

            return View("FollowingRequests", pageViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PendingRequests()
        {
            var requests = await requestService.GetPendingRequests();
            var viewListRequest = requests.Select(x => new RequestViewModel(x, "PendingRequestsTable")).ToList();

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
        public async Task<IActionResult> Create(string commingViewName)
        {
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var requestViewModel = new RequestEditViewModel(viewListCategory, commingViewName);

            return View("Create", requestViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            string name = User.Identity.Name;
            User user = userService.GetByUserName(name).Result;

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

            if (request.CommingViewName == "MyRequestsTable")
            {
                return RedirectToAction(nameof(MyRequests));
            }
            else if (request.CommingViewName == "RequestsTable")
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Upvote(int requestId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await requestService.Like(requestId, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Downvote(int requestId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await requestService.Dislike(requestId, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Follow(int requestId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await requestService.Follow(requestId, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Unfollow(int requestId, string commingViewName)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            await requestService.UnFollow(requestId, userId);

            if (commingViewName == "FollowingRequestsTable")
            {
                return RedirectToAction(nameof(FollowingRequests));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string commingViewName)
        {
            var request = await requestService.GetById(id);

            if (request == null)
            {
                return View("404");
            }

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var viewModel = new RequestEditViewModel(request, viewListCategory, commingViewName);

            if (commingViewName == "PendingRequestsTable")
            {
                return View("EditPendingRequest", viewModel);
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

            if (oldStatus != request.Status && emails.Count != 0)
            {
                var message = new Message(emails,emails, "Status Changed", "Request`s status is changed.");
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


            if (request.CommingViewName == "PendingRequestsTable")
            {
                var requests = await requestService.GetPendingRequests();
                var viewListRequest = requests.Select(x => new RequestViewModel(x, "PendingRequestsTable")).ToList();

                var categories = await categoryService.GetAll();
                var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

                var pageViewModel = new RequestPageViewModel(viewListRequest, viewListCategory);

                return View("PendingRequests", pageViewModel);
            }
            else if (request.CommingViewName == "RequestsTable")
            {
                return RedirectToAction(nameof(Index));
            }
            else if (request.CommingViewName == "MyRequestsTable")
            {
                return RedirectToAction(nameof(MyRequests));
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id, string commingViewName)
        {
            var request = await requestService.GetById(id);

            if (request == null)
            {
                return View("404");
            }

            RequestViewModel viewModel = new(request);
            viewModel.CommingViewName = commingViewName;

            return PartialView("_ConfirmDeleteRequest", viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int requestId, string commingViewName)
        {
            var request = await requestService.GetById(requestId);

            if (request == null)
            {
                return View("404");
            }

            await requestService.Delete(requestId);

            var categories = await categoryService.GetAll();
            var categoriesViewModel = categories.Select(x => new CategoryViewModel(x)).ToList();

            if (commingViewName == "MyRequestsTable")
            {
                var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

                var requests = await requestService.GetMyRequests(userId);
                var requestsViewModel = requests.Select(x => new RequestViewModel(x, "MyRequestsTable")).ToList();

                var viewModel = new RequestPageViewModel(requestsViewModel, categoriesViewModel);

                return PartialView("_MyRequestsTable", viewModel);
            }
            else if (commingViewName == "PendingRequestsTable")
            {
                var requests = await requestService.GetPendingRequests();
                var requestsViewModel = requests.Select(x => new RequestViewModel(x, "PendingRequestsTable")).ToList();

                var viewModel = new RequestPageViewModel(requestsViewModel, categoriesViewModel);

                return PartialView("_PendingRequestsTable", viewModel);
            }
            else
            {
                return View("404");
            }
        }
    }
}
