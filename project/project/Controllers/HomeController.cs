using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using project.Models;
using project.Models.Repository;
using project.ViewModels;
using System.IO;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        public IRepository<CallBack> callBackRepository { get; }
        public IRepository<Contact> contactRepository { get; }
        public IRepository<Car> CarRepository { get; }
        public IRepository<Rent> RentRepository { get; }
        public IRepository<Sale> SaleRepository { get; }
        public IRepository<Lastnewsupdate> LastnewsupdateRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public HomeController(IRepository<Car> carRepository, IRepository<Rent> rentRepository, IRepository<Sale> saleRepository, IRepository<Lastnewsupdate> lastnewsupdateRepository, IHostingEnvironment HostingEnvironment,
            IRepository<CallBack> CallBackRepository, IRepository<Contact> ContactRepository)
        {
            CarRepository = carRepository;
            RentRepository = rentRepository;
            SaleRepository = saleRepository;
            LastnewsupdateRepository = lastnewsupdateRepository;
            this.HostingEnvironment = HostingEnvironment;
            callBackRepository = CallBackRepository;
            contactRepository = ContactRepository;
        }
        public IActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.MostCarResearch = CarRepository.ViewFrontClinet().Take(8).ToList();
            model.lastNew = LastnewsupdateRepository.ViewFrontClinet().OrderByDescending(e => e.LastnewsupdateDate).Take(3).ToList();
            return View(model);
        }

        public IActionResult addCar()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }
        public IActionResult autopart()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }
        public IActionResult CallBack()
        {
            HomeModel model = new HomeModel()
            {
                lstCallBack = callBackRepository.ViewFrontClinet().ToList()
            };
            return View(model);
        }
        // POST: CallBackController/Create
        [HttpPost]
        public ActionResult CallBack(HomeModel dataViewModel)
        {


            CallBack data = new CallBack()
            {
                CallBackId = dataViewModel.CallBack.CallBackId,
                CallBackName = dataViewModel.CallBack.CallBackName,
                CallBackPhone = dataViewModel.CallBack.CallBackPhone,
                CallBackEmail = dataViewModel.CallBack.CallBackEmail,
                CallBackChooseService = dataViewModel.CallBack.CallBackChooseService,
                CallBackComment = dataViewModel.CallBack.CallBackComment,
                CreateDate = DateTime.UtcNow,
                CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
            };
            callBackRepository.Add(data);
            return RedirectToAction(nameof(CallBack));
            

        }
        public IActionResult Contact()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(HomeModel dataViewModel)
        {
            try
            {
                Contact data = new Contact()
                {
                    ContactId = dataViewModel.Contact.ContactId,
                    ContactEmail = dataViewModel.Contact.ContactEmail,
                    ContactName = dataViewModel.Contact.ContactName,
                    ContactQuestion = dataViewModel.Contact.ContactQuestion,
                    ContactSubject = dataViewModel.Contact.ContactSubject,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                };
                contactRepository.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addCar(HomeModel dataViewModel)
        {
            try
            {
                string imageName = "", imageName2 = "", imageName3 = "";
                string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/AddCar");
                FileInfo fileInfo;

                if (dataViewModel.Car.UploadImage != null)
                {
                    fileInfo = new FileInfo(dataViewModel.Car.UploadImage.FileName);
                    imageName = "AddCar" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName);
                    dataViewModel.Car.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                if (dataViewModel.Car.UploadImage2 != null)
                {
                    fileInfo = new FileInfo(dataViewModel.Car.UploadImage2.FileName);
                    imageName2 = "AddCar2" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName2);
                    dataViewModel.Car.UploadImage2.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                if (dataViewModel.Car.UploadImage3 != null)
                {
                    fileInfo = new FileInfo(dataViewModel.Car.UploadImage3.FileName);
                    imageName3 = "AddCar3" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName3);
                    dataViewModel.Car.UploadImage3.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                Car data = new Car()
                {
                    CarId = dataViewModel.Car.CarId,
                    CarGear = dataViewModel.Car.CarGear,
                    CarImgUrl1 = (string.IsNullOrEmpty(imageName) ? null : imageName),
                    CarImgUrl2 = (string.IsNullOrEmpty(imageName2) ? null : imageName2),
                    CarImgUrl3 = (string.IsNullOrEmpty(imageName3) ? null : imageName3),
                    CarPrice = dataViewModel.Car.CarPrice,
                    CarWalking = dataViewModel.Car.CarWalking,
                    CarTypeModel = dataViewModel.Car.CarTypeModel,
                    CarManufacturingyear = dataViewModel.Car.CarManufacturingyear,
                    CarBrand = dataViewModel.Car.CarBrand,
                    CarColor = dataViewModel.Car.CarColor,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    IsActive = true,
                    IsDelete = false,
                    IsRent = false,
                    IsSaled = false,
                    AppUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    RentOrSale = dataViewModel.Car.RentOrSale,
                };
                CarRepository.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        public IActionResult Rent(int id)
        {
            if (id == 0 || id == null)
            {
                HomeModel model = new HomeModel();
                model.ListCar = CarRepository.ViewFrontClinet().Where(c => c.RentOrSale == 2).ToList();
                return View(model);
            }
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var car = CarRepository.Find(id);
            car.IsRent = true;
            car.EditDate = DateTime.UtcNow;
            car.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CarRepository.Update(id, car);
            Rent r = new Rent()
            {
                CarId = id,
                UserRentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                RentDate = DateTime.UtcNow
            };
            RentRepository.Add(r);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Sale(int id)
        {
            if (id == 0 || id == null)
            {
                HomeModel model = new HomeModel();
                model.ListCar = CarRepository.ViewFrontClinet().Where(c => c.RentOrSale == 1).ToList();
                return View(model);
            }
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var car = CarRepository.Find(id);
            car.IsSaled = true;
            car.EditDate = DateTime.UtcNow;
            car.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CarRepository.Update(id, car);
            Sale s = new Sale()
            {
                CarId = id,
                UserSaleId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                SaleDate = DateTime.UtcNow,
            };
            SaleRepository.Add(s);
            return RedirectToAction(nameof(Index));
        }
    }
}
