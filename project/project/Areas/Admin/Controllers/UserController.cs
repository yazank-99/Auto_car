using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using project.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        public UserManager<AppUser> UserManager { get; }
        public RoleManager<AppRole> RoleManager { get; }

        public UserController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public IActionResult Index()
        {
            IList<AppUser> dataList = UserManager.Users.Where(data => !data.IsDelete && data.Id.ToString() != User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            return View(dataList);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Active(string id)
        {
            AppUser data = UserManager.Users.SingleOrDefault(data => data.Id.ToString() == id);
            data.IsActive = !data.IsActive;
            data.EditDate = DateTime.UtcNow;
            data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await UserManager.UpdateAsync(data);
            return RedirectToAction(nameof(Index));
        }
        // GET: UserController/Create
        public IActionResult Create()
        {
            ViewBag.Roles = RoleManager.Roles;
            RegisterViewModel dataViewModel = new RegisterViewModel();
            dataViewModel.Email = "";
            dataViewModel.Role = "";
            return View(dataViewModel);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel dataViewModel)
        {
            try
            {
                ViewBag.Roles = RoleManager.Roles;
                AppUser user = new AppUser()
                {
                    Email = dataViewModel.Email,
                    UserName = dataViewModel.Email,
                    IsActive = true,
                    IsDelete = false,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                if (!ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(dataViewModel.Email))
                    {
                        ModelState.AddModelError("", "Please Enter The Email");
                    }
                    if (string.IsNullOrEmpty(dataViewModel.Password) || string.IsNullOrEmpty(dataViewModel.ConfirmPassword))
                    {
                        ModelState.AddModelError("", "Please Enter The Password And Confirm It");
                    }
                    return View(dataViewModel);
                }
                var userExist = UserManager.Users.SingleOrDefault(u => u.UserName.ToUpper() == user.UserName.ToUpper());


                if (userExist != null && !string.IsNullOrEmpty(userExist.UserName))
                {
                    ModelState.AddModelError("", "This user is already exist");
                }

                if (userExist == null)
                {
                    var Result = await UserManager.CreateAsync(user, dataViewModel.Password);
                    if (Result.Succeeded)
                    {
                        var userCreated = await UserManager.Users.FirstOrDefaultAsync(e => e.UserName == dataViewModel.Email);
                        await UserManager.AddToRoleAsync(userCreated, dataViewModel.Role);
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View(dataViewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Roles = RoleManager.Roles;
            AppUser data = UserManager.Users.SingleOrDefault(data => data.Id.ToString() == id);
            var roles = await UserManager.GetRolesAsync(data);

            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                Email = data.Email,
                Role = roles[0],
                Id = id
            };
            return View(registerViewModel);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegisterViewModel dataViewModel)
        {
            try
            {
                ViewBag.Roles = RoleManager.Roles;
                if (UserManager.Users.SingleOrDefault(dataU => dataU.Email.ToUpper() == dataViewModel.Email.ToUpper() && dataU.Id.ToString() != dataViewModel.Id) != null)
                {
                    ModelState.AddModelError("", "This email is already exist");
                    return View(dataViewModel);
                }
                AppUser appUser = await UserManager.FindByEmailAsync(dataViewModel.Email);
                appUser.Email = dataViewModel.Email;
                appUser.UserName = dataViewModel.Email;
                appUser.EditDate = DateTime.UtcNow;
                appUser.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var roles = await UserManager.GetRolesAsync(appUser);
                await UserManager.RemoveFromRoleAsync(appUser, roles.ElementAt(0));
                var r = await UserManager.UpdateAsync(appUser);
                var userUpdated = await UserManager.Users.FirstOrDefaultAsync(e => e.UserName == dataViewModel.Email);
                await UserManager.AddToRoleAsync(userUpdated, dataViewModel.Role);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            AppUser data = UserManager.Users.SingleOrDefault(data => data.Id.ToString() == id);
            data.IsDelete = true;
            data.EditDate = DateTime.UtcNow;
            data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await UserManager.UpdateAsync(data);
            return RedirectToAction(nameof(Index));
        }
    }
}
