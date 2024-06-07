using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data;
using project.Models.Repository;
using project.Models;
using project.ViewModels;
using System.Linq;

namespace project.Areas.customer.Controllers
{
    [Area("customer")]
    public class UserRegisterController : Controller
    {
        public IRepository<UserRegister> UserRegisterRepository { get; }

        public UserRegisterController(IRepository<UserRegister> UserRegisterRepository)
        {
            this.UserRegisterRepository = UserRegisterRepository;
        }
        // GET: UserRegisterController
        public ActionResult Index()
        {
            UserRegisterModel dataViewModel = new UserRegisterModel();
            return View(dataViewModel);
        }
        public ActionResult login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Register");
            }
        }

        // POST: UserRegisterController/Create
        [HttpPost]
        public ActionResult login(UserRegisterModel dataViewModel)
        {

          

            if (ModelState.IsValid)
            {
                var model = new UserRegisterModel();
                {
                    model.Email = dataViewModel.Email;
                    model.Password = dataViewModel.Password;

                }

                //using (AppDbContext db = new AppDbContext())
                //{
                //    var obj = UserRegisterRepository.View().ToList().Find(x=>x.Email==model.Email&& x.Password==model.Password);
                //    if (obj != null)
                //    {
                //        HttpContext.Session.SetString("Email", obj.Email.ToString());
                //        return RedirectToAction("Register");
                //    }
                //}

                var CheckUser = UserRegisterRepository.View().ToList().Find(x => x.Email == model.Email && x.Password == model.Password);
                if (CheckUser != null)
                {
                    HttpContext.Session.SetString("UserRegisterId", CheckUser.Id.ToString());
                    return RedirectToAction("Register");
                }
            }


            return RedirectToAction("Login");
        }

        // GET: UserRegisterController/Create
        public ActionResult Register()
        {
            UserRegisterModel dataViewModel = new UserRegisterModel();
            return View(dataViewModel);
        }

        // POST: UserRegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterModel dataViewModel)
        {
            try
            {

                UserRegister data = new UserRegister()
                {
                    Id = dataViewModel.Id,
                    Email = dataViewModel.Email,
                    Password = dataViewModel.Password,
                    ConfirmPassword = dataViewModel.ConfirmPassword

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
                UserRegisterRepository.Add(data);
                return RedirectToAction(nameof(login));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Email");

            return RedirectToAction("Login");
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
