using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MelonBookshelf.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var data = await userService.GetAll();
            var resources = data.Select(x => new UserViewModel(x)).ToList();
            var viewModel = new UserPageViewModel(resources);
            return View("User", viewModel);
        }
        public async Task<IActionResult> Details(string id)
        {
            var data = await userService.GetById(id);
            UserViewModel user = new(data);
            return View("Details", user);

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
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userService.GetById(id);
            UserViewModel model = new UserViewModel(user);
            if (user == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, User user)
        {
            user.Id = id;
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            await userService.Update(id, user);
            return RedirectToAction(nameof(Index));
        }

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
