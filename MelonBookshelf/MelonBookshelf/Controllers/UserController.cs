﻿using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelf.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await userService.GetAll();
            return View("User", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await userService.Add(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userService.GetById(id);
            if (user == null)
            {
                return View("NotFound");
            }
            return View(user);
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
            if (user == null)
            {
                return View("NotFound");
            }
            return View(user);
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