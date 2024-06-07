using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        public RoleManager<AppRole> RoleManager { get; }

        public RoleController(RoleManager<AppRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public IActionResult Index()
        {
            IList<AppRole> dataList = RoleManager.Roles.Where(r => r.Name != "admin").ToList();
            return View(dataList);
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            AppRole AppRole = new AppRole();
            return View(AppRole);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppRole AppRole)
        {
            try
            {
                if (string.IsNullOrEmpty(AppRole.Name))
                {
                    ModelState.AddModelError("", "Please Enter The Name");
                }
                if (RoleManager.Roles.SingleOrDefault(u => u.Name.ToUpper() == AppRole.Name.ToUpper() && u.Id != AppRole.Id) != null)
                {
                    ModelState.AddModelError("", "This role is already exist");
                }
                if (ModelState.ErrorCount > 0)
                {
                    return View(AppRole);
                }
                AppRole.NormalizedName = AppRole.Name;
                AppRole.ConcurrencyStamp = Guid.NewGuid().ToString();
                AppRole.CreateDate = DateTime.Now;
                AppRole.CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Result = await RoleManager.CreateAsync(AppRole);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(AppRole);
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            AppRole data = RoleManager.Roles.SingleOrDefault(data => data.Id.ToString() == id);
            return View(data);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppRole AppRole)
        {
            try
            {
                if (string.IsNullOrEmpty(AppRole.Name))
                {
                    ModelState.AddModelError("", "Please Enter The Name");
                }
                if (RoleManager.Roles.SingleOrDefault(u => u.Name.ToUpper() == AppRole.Name.ToUpper() && u.Id != AppRole.Id) != null)
                {
                    ModelState.AddModelError("", "This role is already exist");
                }
                if (ModelState.ErrorCount > 0)
                {
                    return View(AppRole);
                }
                AppRole.NormalizedName = AppRole.Name;
                AppRole.EditDate = DateTime.Now;
                AppRole.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                AppRole.ConcurrencyStamp = Guid.NewGuid().ToString();
                var Result = await RoleManager.UpdateAsync(AppRole);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(AppRole);
            }
            catch
            {
                return View();
            }
        }
    }
}
