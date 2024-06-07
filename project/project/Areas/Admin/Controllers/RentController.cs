using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project.Models;
using project.Models.Repository;
using project.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Claims;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class RentController : Controller
    {
        public IRepository<Rent> RentRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public RentController(IRepository<Rent> RentRepository,
            IHostingEnvironment HostingEnvironment)
        {
            this.RentRepository = RentRepository;
            this.HostingEnvironment = HostingEnvironment;
        }
        // GET: RentController
        public ActionResult Index()
        {
            IList<Rent> dataList = RentRepository.View();
            IList<RentModel> dataViewModelList = new List<RentModel>();
            foreach (var data in dataList)
            {
                RentModel dataViewModel = new RentModel()
                {
                    RentId = data.RentId,
                    RentDate=data.RentDate,
                    AppUser = data.AppUser,
                    Car = data.Car,
                    IsActive = data.IsActive,
                };
                dataViewModelList.Add(dataViewModel);
            }
            return View(dataViewModelList);
        }

        //public ActionResult Active(int id)
        //{
        //    RentRepository.Active(id, new Rent()
        //    {
        //        EditDate = DateTime.UtcNow,
        //        EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
        //    });
        //    return RedirectToAction(nameof(Index));
        //}

        // GET: RentController/Create
        //public ActionResult Create()
        //{
        //    RentModel dataViewModel = new RentModel();
        //    return View(dataViewModel);
        //}

        //// POST: RentController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(RentModel dataViewModel)
        //{
        //    try
        //    {
        //        string imageName = "", imageName2 = "", imageName3 = "";
        //        string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/Rent");
        //        FileInfo fileInfo;
        //        if (dataViewModel.UploadImage != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage.FileName);
        //            imageName = "RentImg" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName);
        //            dataViewModel.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        if (dataViewModel.UploadImage2 != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage2.FileName);
        //            imageName2 = "RentImg2" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName2);
        //            dataViewModel.UploadImage2.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        if (dataViewModel.UploadImage3 != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage3.FileName);
        //            imageName3 = "RentImg3" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName3);
        //            dataViewModel.UploadImage3.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        Rent data = new Rent()
        //        {
        //            RentId = dataViewModel.RentId,
        //            RentDate = dataViewModel.RentDate,
        //            RentImgUrl1 = (string.IsNullOrEmpty(imageName) ? null : imageName),
        //            RentImgUrl2 = (string.IsNullOrEmpty(imageName2) ? null : imageName2),
        //            RentImgUrl3 = (string.IsNullOrEmpty(imageName3) ? null : imageName3),
        //            CreateDate = DateTime.UtcNow,
        //            CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
        //            IsActive = true,
        //            IsDelete = false
        //        };
        //        RentRepository.Add(data);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: RentController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    Rent data = RentRepository.Find(id);
        //    RentModel dataViewModel = new RentModel()
        //    {
        //        RentId=data.RentId,
        //        RentDate = data.RentDate,
        //        RentImgUrl1=data.RentImgUrl1,
        //        RentImgUrl2=data.RentImgUrl2,
        //        RentImgUrl3=data.RentImgUrl3,
        //    };
        //    return View(dataViewModel);
        //}

        //// POST: RentController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, RentModel dataViewModel)
        //{
           
        //        try
        //        {
        //            string imageName = dataViewModel.RentImgUrl1,
        //                imageName2 = dataViewModel.RentImgUrl2,
        //                imageName3 = dataViewModel.RentImgUrl3;
        //            string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/Rent");
        //            FileInfo fileInfo;
        //            if (dataViewModel.UploadImage != null)
        //            {
        //                fileInfo = new FileInfo(dataViewModel.UploadImage.FileName);
        //                imageName = "RentImg1" + Guid.NewGuid() + fileInfo.Extension;
        //                string fullPath = Path.Combine(imagePath, imageName);
        //                dataViewModel.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
        //            }
        //            if (dataViewModel.UploadImage2 != null)
        //            {
        //                fileInfo = new FileInfo(dataViewModel.UploadImage2.FileName);
        //                imageName2 = "RentImg2" + Guid.NewGuid() + fileInfo.Extension;
        //                string fullPath = Path.Combine(imagePath, imageName2);
        //                dataViewModel.UploadImage2.CopyTo(new FileStream(fullPath, FileMode.Create));
        //            }
        //            if (dataViewModel.UploadImage3 != null)
        //            {
        //                fileInfo = new FileInfo(dataViewModel.UploadImage3.FileName);
        //                imageName3 = "RentImg3" + Guid.NewGuid() + fileInfo.Extension;
        //                string fullPath = Path.Combine(imagePath, imageName3);
        //                dataViewModel.UploadImage3.CopyTo(new FileStream(fullPath, FileMode.Create));
        //            }
        //            Rent data = RentRepository.Find(id);
        //            data.RentId = dataViewModel.RentId;
        //            data.RentDate = dataViewModel.RentDate;
        //            data.RentImgUrl1 = (string.IsNullOrEmpty(imageName) ? null : imageName);
        //            data.RentImgUrl2 = (string.IsNullOrEmpty(imageName2) ? null : imageName2);
        //            data.RentImgUrl3 = (string.IsNullOrEmpty(imageName3) ? null : imageName3);
        //            data.EditDate = DateTime.UtcNow;
        //            data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //            RentRepository.Update(id, data);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
            

        public ActionResult Delete(int id)
        {
            RentRepository.Delete(id, new Rent
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
