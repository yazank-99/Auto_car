using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class SaleController : Controller
    {
        public IRepository<Sale> SaleRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public SaleController(IRepository<Sale> SaleRepository,
            IHostingEnvironment HostingEnvironment)
        {
            this.SaleRepository = SaleRepository;
            this.HostingEnvironment = HostingEnvironment;
        }
        // GET: SaleController
        public ActionResult Index()
        {
            IList<Sale> dataList = SaleRepository.View();
            IList<SaleModel> dataViewModelList = new List<SaleModel>();
            foreach (var data in dataList)
            {
                SaleModel dataViewModel = new SaleModel()
                {
                    SaleId = data.SaleId,
                    SaleDate = data.SaleDate,
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
        //    SaleRepository.Active(id, new Sale()
        //    {
        //        EditDate = DateTime.UtcNow,
        //        EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
        //    });
        //    return RedirectToAction(nameof(Index));
        //}

        // GET: SaleController/Create
        //public ActionResult Create()
        //{
        //    SaleModel dataViewModel = new SaleModel();
        //    return View(dataViewModel);
        //}

        //// POST: SaleController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(SaleModel dataViewModel)
        //{
        //    try
        //    {
        //        string imageName = "", imageName2 = "", imageName3 = "";
        //        string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/Sale");
        //        FileInfo fileInfo;
        //        if (dataViewModel.UploadImage != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage.FileName);
        //            imageName = "SaleImg" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName);
        //            dataViewModel.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        if (dataViewModel.UploadImage2 != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage2.FileName);
        //            imageName2 = "SaleImg2" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName2);
        //            dataViewModel.UploadImage2.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        if (dataViewModel.UploadImage3 != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage3.FileName);
        //            imageName3 = "SaleImg3" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName3);
        //            dataViewModel.UploadImage3.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        Sale data = new Sale()
        //        {
        //            SaleId = dataViewModel.SaleId,
        //            SaleWalking = dataViewModel.SaleWalking,
        //            SaleImgUrl1 = (string.IsNullOrEmpty(imageName) ? null : imageName),
        //            SaleImgUrl2 = (string.IsNullOrEmpty(imageName2) ? null : imageName2),
        //            SaleImgUrl3 = (string.IsNullOrEmpty(imageName3) ? null : imageName3),
        //            CreateDate = DateTime.UtcNow,
        //            CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
        //            IsActive = true,
        //            IsDelete = false
        //        };
        //        SaleRepository.Add(data);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SaleController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    Sale data = SaleRepository.Find(id);
        //    SaleModel dataViewModel = new SaleModel()
        //    {
        //        SaleId= data.SaleId,
        //        SaleWalking = data.SaleWalking,
        //        SaleImgUrl1 = data.SaleImgUrl1,
        //        SaleImgUrl2 = data.SaleImgUrl2,
        //        SaleImgUrl3 = data.SaleImgUrl3,
        //    };
        //    return View(dataViewModel);
        //}

        //// POST: SaleController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, SaleModel dataViewModel)
        //{
        //    try
        //    {
        //        string imageName = dataViewModel.SaleImgUrl1,
        //            imageName2 = dataViewModel.SaleImgUrl2,
        //            imageName3 = dataViewModel.SaleImgUrl3;
        //        string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/Sale");
        //        FileInfo fileInfo;
        //        if (dataViewModel.UploadImage != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage.FileName);
        //            imageName = "SaleImage1" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName);
        //            dataViewModel.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        if (dataViewModel.UploadImage2 != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage2.FileName);
        //            imageName2 = "SaleImage2" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName2);
        //            dataViewModel.UploadImage2.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        if (dataViewModel.UploadImage3 != null)
        //        {
        //            fileInfo = new FileInfo(dataViewModel.UploadImage3.FileName);
        //            imageName3 = "SaleImage3" + Guid.NewGuid() + fileInfo.Extension;
        //            string fullPath = Path.Combine(imagePath, imageName3);
        //            dataViewModel.UploadImage3.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        }
        //        Sale data = SaleRepository.Find(id);
        //        data.SaleId = dataViewModel.SaleId;
        //        data.SaleWalking= dataViewModel.SaleWalking;
        //        data.SaleImgUrl1 = (string.IsNullOrEmpty(imageName) ? null : imageName);
        //        data.SaleImgUrl2 = (string.IsNullOrEmpty(imageName2) ? null : imageName2);
        //        data.SaleImgUrl3 = (string.IsNullOrEmpty(imageName3) ? null : imageName3);
        //        data.EditDate = DateTime.UtcNow;
        //        data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        SaleRepository.Update(id, data);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: SaleController/Delete/5
        public ActionResult Delete(int id)
        {
            SaleRepository.Delete(id, new Sale
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
