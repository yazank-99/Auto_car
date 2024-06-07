using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace project.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public RoleManager<AppRole> RoleManager { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Username or password failed");
                return View(loginViewModel);
            }
            var userDeactivate = UserManager.Users.SingleOrDefault(u => u.UserName.ToUpper() == loginViewModel.Email.ToUpper());
            if (userDeactivate != null && !userDeactivate.IsDelete && userDeactivate.IsActive)
            {
                var Result = await SignInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, isPersistent: loginViewModel.RememberMe, false);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Username or password incorrect");
            }
            else
            {
                ModelState.AddModelError("", "Username or password incorrect");
            }
            return View(loginViewModel);
        }
        public async Task<IActionResult> Register()
        {
            return View(new RegisterViewModel()
            {
                Email ="",
                Role = ""
            });
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            AppUser user = new AppUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                IsActive = true,
                IsDelete = false,
                CreateDate = DateTime.UtcNow
            };
            if(RoleManager.Roles.FirstOrDefault(e=>e.Name == "customer") == null)
            {
                await RoleManager.CreateAsync(new AppRole()
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreateDate = DateTime.UtcNow,
                    Name = "customer",
                    NormalizedName = "customer"
                });
            }
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(registerViewModel.Email))
                {
                    ModelState.AddModelError("", "Please Enter The Email");
                }
                if (string.IsNullOrEmpty(registerViewModel.Password) || string.IsNullOrEmpty(registerViewModel.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Please Enter The Password And Confirm It");
                }
                return View(registerViewModel);
            }
            var userExist = UserManager.Users.SingleOrDefault(u => u.UserName.ToUpper() == user.UserName.ToUpper());


            if (userExist != null && !string.IsNullOrEmpty(userExist.UserName))
            {
                ModelState.AddModelError("", "This user is already exist");
            }

            if (userExist == null)
            {
                var Result = await UserManager.CreateAsync(user, registerViewModel.Password);
                if (Result.Succeeded)
                {
                    var userCreated = await UserManager.Users.FirstOrDefaultAsync(e => e.UserName == registerViewModel.Email);
                    await UserManager.AddToRoleAsync(userCreated, "customer");
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(registerViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
