using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Models.Repository;
using project.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CarController : Controller
    {
        public IRepository<Car> AddCarRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public CarController(IRepository<Car> AddCarRepository,
            IHostingEnvironment HostingEnvironment)
        {
            this.AddCarRepository = AddCarRepository;
            this.HostingEnvironment = HostingEnvironment;
        }
        // GET: AddCarController
        public ActionResult Index()
        {
            IList<Car> dataList = AddCarRepository.View();
            IList<CarModel> dataViewModelList = new List<CarModel>();
            foreach (var data in dataList)
            {
                CarModel dataViewModel = new CarModel()
                {
                    CarId = data.CarId,
                    CarImgUrl1 = data.CarImgUrl1,
                    CarImgUrl2 = data.CarImgUrl2,
                    CarImgUrl3 = data.CarImgUrl3,
                    CarPrice = data.CarPrice,
                    CarGear = data.CarGear,
                    CarWalking = data.CarWalking,
                    CarBrand = data.CarBrand,
                    CarColor = data.CarColor,
                    CarManufacturingyear = data.CarManufacturingyear,
                    CarTypeModel = data.CarTypeModel,
                    IsActive = data.IsActive,
                    IsRent = data.IsRent,
                    IsSaled = data.IsSaled,
                    AppUserId = data.AppUserId,
                    AppUser = data.AppUser,
                    RentOrSale = data.RentOrSale,
                };
                dataViewModelList.Add(dataViewModel);
            }
            return View(dataViewModelList);
        }

        // GET: AddCarController/Details/5
        public ActionResult Active(int id)
        {
            AddCarRepository.Active(id, new Car()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }


        // GET: AddCarController/Create
        public ActionResult Create()
        {
            CarModel dataViewModel = new CarModel();
            return View(dataViewModel);
        }

        // POST: AddCarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarModel dataViewModel)
        {
            try
            {
                string imageName = "", imageName2 = "", imageName3 = "";
                string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/AddCar");
                FileInfo fileInfo;

                if (dataViewModel.UploadImage != null)
                {
                    fileInfo = new FileInfo(dataViewModel.UploadImage.FileName);
                    imageName = "AddCar" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName);
                    dataViewModel.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                if (dataViewModel.UploadImage2 != null)
                {
                    fileInfo = new FileInfo(dataViewModel.UploadImage2.FileName);
                    imageName2 = "AddCar2" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName2);
                    dataViewModel.UploadImage2.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                if (dataViewModel.UploadImage3 != null)
                {
                    fileInfo = new FileInfo(dataViewModel.UploadImage3.FileName);
                    imageName3 = "AddCar3" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName3);
                    dataViewModel.UploadImage3.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                Car data = new Car()
                {
                    CarId = dataViewModel.CarId,
                    CarGear = dataViewModel.CarGear,
                    CarImgUrl1 = (string.IsNullOrEmpty(imageName) ? null : imageName),
                    CarImgUrl2 = (string.IsNullOrEmpty(imageName2) ? null : imageName2),
                    CarImgUrl3 = (string.IsNullOrEmpty(imageName3) ? null : imageName3),
                    CarPrice = dataViewModel.CarPrice,
                    CarWalking = dataViewModel.CarWalking,
                    CarTypeModel = dataViewModel.CarTypeModel,
                    CarManufacturingyear = dataViewModel.CarManufacturingyear,
                    CarBrand = dataViewModel.CarBrand,
                    CarColor = dataViewModel.CarColor,
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    IsActive = true,
                    IsDelete = false,
                    IsRent = false,
                    IsSaled = false,
                    AppUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    RentOrSale = dataViewModel.RentOrSale,
                };
                AddCarRepository.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AddCarController/Edit/5
        public ActionResult Edit(int id)
        {

            Car data = AddCarRepository.Find(id);
            CarModel dataViewModel = new CarModel()
            {
                CarId = data.CarId,
                CarWalking = data.CarWalking,
                CarGear = data.CarGear,
                CarPrice = data.CarPrice,
                CarImgUrl1 = data.CarImgUrl1,
                CarImgUrl2 = data.CarImgUrl2,
                CarImgUrl3 = data.CarImgUrl3,
                CarBrand = data.CarBrand,
                CarColor = data.CarColor,
                CarManufacturingyear = data.CarManufacturingyear,
                CarTypeModel = data.CarTypeModel,
                RentOrSale = data.RentOrSale,
            };
            return View(dataViewModel);
        }

        // POST: AddCarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CarModel dataViewModel)
        {
            try
            {
                string imageName = dataViewModel.CarImgUrl1,
                   imageName2 = dataViewModel.CarImgUrl2,
                   imageName3 = dataViewModel.CarImgUrl3;
                string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/AddCar");
                FileInfo fileInfo;
                if (dataViewModel.UploadImage != null)
                {
                    fileInfo = new FileInfo(dataViewModel.UploadImage.FileName);
                    imageName = "AddCarImag1" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName);
                    dataViewModel.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                if (dataViewModel.UploadImage2 != null)
                {
                    fileInfo = new FileInfo(dataViewModel.UploadImage2.FileName);
                    imageName2 = "AddCarImag2" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName2);
                    dataViewModel.UploadImage2.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                if (dataViewModel.UploadImage3 != null)
                {
                    fileInfo = new FileInfo(dataViewModel.UploadImage3.FileName);
                    imageName3 = "AddCarImag3" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName3);
                    dataViewModel.UploadImage3.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                Car data = AddCarRepository.Find(id);
                data.CarId = dataViewModel.CarId;
                data.CarGear = dataViewModel.CarGear;
                data.CarWalking = dataViewModel.CarWalking;
                data.CarPrice = dataViewModel.CarPrice;
                data.CarColor = dataViewModel.CarColor;
                data.CarBrand = dataViewModel.CarBrand;
                data.CarManufacturingyear = dataViewModel.CarManufacturingyear;
                data.CarTypeModel = dataViewModel.CarTypeModel;
                data.CarImgUrl1 = (string.IsNullOrEmpty(imageName) ? null : imageName);
                data.CarImgUrl2 = (string.IsNullOrEmpty(imageName2) ? null : imageName2);
                data.CarImgUrl3 = (string.IsNullOrEmpty(imageName3) ? null : imageName3);
                data.EditDate = DateTime.UtcNow;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.RentOrSale = dataViewModel.RentOrSale;
                AddCarRepository.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            AddCarRepository.Delete(id, new Car
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
