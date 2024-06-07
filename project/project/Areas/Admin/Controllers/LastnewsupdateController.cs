using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
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
    public class LastnewsupdateController : Controller
    {
        public IRepository<Lastnewsupdate> LastnewsupdateRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public LastnewsupdateController(IRepository<Lastnewsupdate> LastnewsupdateRepository,IHostingEnvironment HostingEnvironment)
        {
            this.LastnewsupdateRepository = LastnewsupdateRepository;
            this.HostingEnvironment = HostingEnvironment;
        }
        // GET: LastnewsupdateController
        public ActionResult Index()
        {
            IList<Lastnewsupdate> dataList = LastnewsupdateRepository.View();
            IList<LastnewsupdateModel> dataViewModelList = new List<LastnewsupdateModel>();
            foreach (var data in dataList)
            {
                LastnewsupdateModel dataViewModel = new LastnewsupdateModel()
                {
                   LastnewsupdateId=data.LastnewsupdateId,
                   LastnewsupdateName=data.LastnewsupdateName,
                   LastnewsupdateTitle = data.LastnewsupdateTitle,
                   LastnewsupdateDesc=data.LastnewsupdateDesc,
                   LastnewsupdateDate=data.LastnewsupdateDate,
                   LastnewsupdateImgeUrl=data.LastnewsupdateImgeUrl,
                    IsActive = data.IsActive
                };
                dataViewModelList.Add(dataViewModel);
            }
            return View(dataViewModelList);
        }

        public ActionResult Active(int id)
        {
            LastnewsupdateRepository.Active(id, new Lastnewsupdate()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
        // GET: LastnewsupdateController/Create
        public ActionResult Create()
        {
            LastnewsupdateModel dataViewModel = new LastnewsupdateModel();
            return View(dataViewModel);
        }

        // POST: LastnewsupdateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LastnewsupdateModel dataViewModel)
        {
            try
            {
                string imageName = "";
                if (dataViewModel.UploadImage != null)
                {
                    string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/Lastnewsupdate");
                    FileInfo fileInfo = new FileInfo(dataViewModel.UploadImage.FileName);
                    imageName = "Lastnewsupdate" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName);
                    dataViewModel.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                Lastnewsupdate data = new Lastnewsupdate()
                {
                    LastnewsupdateId = dataViewModel.LastnewsupdateId,
                    LastnewsupdateName = dataViewModel.LastnewsupdateName,
                    LastnewsupdateDate=dataViewModel.LastnewsupdateDate,
                    LastnewsupdateTitle = dataViewModel.LastnewsupdateTitle,
                    LastnewsupdateDesc = dataViewModel.LastnewsupdateDesc,
                    LastnewsupdateImgeUrl = (string.IsNullOrEmpty(imageName) ? null : imageName),
                    CreateDate = DateTime.UtcNow,
                    CreateUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    IsActive = true,
                    IsDelete = false
                };
                LastnewsupdateRepository.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LastnewsupdateController/Edit/5
        public ActionResult Edit(int id)
        {
            Lastnewsupdate data = LastnewsupdateRepository.Find(id);
            LastnewsupdateModel dataViewModel = new LastnewsupdateModel()
            {
                LastnewsupdateId = data.LastnewsupdateId,
                LastnewsupdateName = data.LastnewsupdateName,
                LastnewsupdateDate = data.LastnewsupdateDate,
                LastnewsupdateTitle = data.LastnewsupdateTitle,
                LastnewsupdateDesc = data.LastnewsupdateDesc,
                LastnewsupdateImgeUrl=data.LastnewsupdateImgeUrl
            };
            return View(dataViewModel);
        }

        // POST: LastnewsupdateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LastnewsupdateModel dataViewModel)
        {
            try
            {
                Lastnewsupdate data = LastnewsupdateRepository.Find(id);
                string imageName = dataViewModel.LastnewsupdateImgeUrl;
                if (dataViewModel.UploadImage != null)
                {
                    string imagePath = Path.Combine(HostingEnvironment.WebRootPath, "images/Lastnewsupdate");
                    FileInfo fileInfo = new FileInfo(dataViewModel.UploadImage.FileName);
                    imageName = "LastnewsupdateLogo" + Guid.NewGuid() + fileInfo.Extension;
                    string fullPath = Path.Combine(imagePath, imageName);
                    dataViewModel.UploadImage.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                data.LastnewsupdateId = dataViewModel.LastnewsupdateId;
                data.LastnewsupdateDate = dataViewModel.LastnewsupdateDate;
                data.LastnewsupdateDesc = dataViewModel.LastnewsupdateDesc;
                data.LastnewsupdateName = dataViewModel.LastnewsupdateName;
                data.LastnewsupdateImgeUrl = (string.IsNullOrEmpty(imageName) ? null : imageName);
                data.LastnewsupdateTitle = dataViewModel.LastnewsupdateTitle;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.EditDate = DateTime.UtcNow;
                LastnewsupdateRepository.Update(id,data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            LastnewsupdateRepository.Delete(id, new Lastnewsupdate()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
    }
 }
