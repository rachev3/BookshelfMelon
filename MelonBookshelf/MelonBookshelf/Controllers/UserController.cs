using AutoMapper;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace MelonBookshelf.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IRequestService requestService;
        private readonly ICategoryService categoryService;

        public UserController(IUserService userService, UserManager<User> userManager, SignInManager<User> signInManager, IRequestService requestService, ICategoryService categoryService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.requestService = requestService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await userService.GetAll();
            var resources = data.Select(x => new UserViewModel(x)).ToList();
            var viewModel = new UserPageViewModel(resources);
            return View("User", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string userName)
        {

            var user = await userService.GetByUserName(userName);
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            UserViewModel userViewModel = new UserViewModel(user);

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var followingRequests = await requestService.GetFollowingRequests(userId);
            var viewListFollowingRequest = followingRequests.Select(x => new RequestViewModel(x, "FollowingRequestsTable")).ToList();

            var myRequests = await requestService.GetMyRequests(userId);
            var viewListMyRequest = myRequests.Select(x => new RequestViewModel(x, "MyRequestsTable")).ToList();

            var changePassViewModel = new UserChangePasswordViewModel();
            changePassViewModel.Username = userName;

            var pageViewModel = new UserDetailsWrapperViewModel(userViewModel, viewListFollowingRequest, viewListMyRequest, viewListCategory, changePassViewModel);
            pageViewModel.CommingViewName = "UserDetails";
            return View("Details", pageViewModel);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded && model.CommingViewName=="UserDetails")
                {
                    var viewModel = new UserChangePasswordViewModel();
                    viewModel.Username = model.UserName;
                    return PartialView("_ChangePasswordPartial", viewModel);
                }
                else if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
            }
            
            if(model.CommingViewName == "UserDetails")
            {
                ModelState.AddModelError("", "Wrong Password");

                return PartialView("_PasswordPopUp", model);
            }
            ModelState.AddModelError("", "Invalid Login");
            return View("Login");

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email

                };

                await userManager.CreateAsync(user, model.Password);
                await userManager.AddToRoleAsync(user, "User");

                return RedirectToAction("Login");
            }
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

            foreach (var errorMessage in errorMessages)
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string username, string commingViewName)
        {

            var model = new UserLoginViewModel();
            model.UserName = username;
            model.CommingViewName = commingViewName;
            return PartialView("_PasswordPopUp", model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordConfirm(string newPassword, string username)
        {
            User user = await userService.GetByUserName(username);
            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user, newPassword);

            return RedirectToAction("Logout");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            User user = await userService.GetByUserName(userViewModel.Username);
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            user.PhoneNumber = userViewModel.PhoneNumber;
            await userService.Update(user);


            return RedirectToAction("Details", new { userName = userViewModel.Username });
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userService.GetById(id);
            UserViewModel userViewModel = new(user);
            if (user == null)
            {
                return View("NotFound");
            }
            return View(userViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var user = await userService.GetById(id);
            if (user == null)
            {
                return View("NotFound");
            }

            await userService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
