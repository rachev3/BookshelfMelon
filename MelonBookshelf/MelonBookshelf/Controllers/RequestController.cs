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

        //where do you use this Task?
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

            //fix
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            RequestEditViewModel requestViewModel = new(request, viewListCategory);

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
             //check if u have category in the model
            Category category = null;
            if (request.CategoryId != null)
            {
                category = await categoryService.GetById(request.CategoryId.Value);
            }

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
                category
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
           

            var emails = await requestService.GetFollowersEmails(request.RequestId);


            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();


            Category category = null;
            if (request.CategoryId != null)
            {


                category = await categoryService.GetById(request.CategoryId.Value);
            }
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


            var result = requestService.GetById(request.RequestId).Result;

            //creating resource
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

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPendingRequest(RequestEditViewModel request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            Category category = null;
            if (request.CategoryId != null)
            {


                category = await categoryService.GetById(request.CategoryId.Value);
            }

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
                category
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
