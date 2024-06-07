using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Models.Repository;
using project.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CallBackController : Controller
    {
        public IRepository<CallBack> CallBackRepository { get; }

        public CallBackController(IRepository<CallBack> CallBackRepository)
        {
         
            this.CallBackRepository = CallBackRepository;
        }
        // GET: CallBackController
        public ActionResult Index()
        {
            IList<CallBack> dataList = CallBackRepository.View();
            IList<CallBackModel> dataViewModelList = new List<CallBackModel>();
            foreach (var data in dataList)
            {
                CallBackModel dataViewModel = new CallBackModel()
                {
                    CallBackId = data.CallBackId,
                    CallBackName = data.CallBackName,
                    CallBackChooseService = data.CallBackChooseService,
                    CallBackPhone = data.CallBackPhone,
                    CallBackComment = data.CallBackComment,
                    CallBackEmail = data.CallBackEmail,
                    IsActive = data.IsActive,
                };
                dataViewModelList.Add(dataViewModel);
            }
            return View(dataViewModelList);

        }

        public ActionResult Active(int id)
        {
            CallBackRepository.Active(id, new CallBack()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: CallBackController/Create
        public ActionResult Create()
        {
            CallBackModel dataViewModel = new CallBackModel();
            return View(dataViewModel);
        }

        // POST: CallBackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CallBackModel dataViewModel)
        {
            try
            {
                CallBack data = new CallBack() {
                    CallBackId = dataViewModel.CallBackId,
                    CallBackName = dataViewModel.CallBackName,
                    CallBackEmail = dataViewModel.CallBackEmail,
                    CallBackPhone = dataViewModel.CallBackPhone,
                    CallBackComment = dataViewModel.CallBackComment,
                    CallBackChooseService = dataViewModel.CallBackChooseService,
                    IsActive = true,
                    IsDelete = false,
                    
                };
                CallBackRepository.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallBackController/Edit/5
        public ActionResult Edit(int id)
        {
                CallBack data = CallBackRepository.Find(id);
                CallBackModel dataViewModel = new CallBackModel()
                {
                    CallBackId = data.CallBackId,
                    CallBackName = data.CallBackName,
                    CallBackEmail = data.CallBackEmail,
                    CallBackPhone = data.CallBackPhone,
                    CallBackComment = data.CallBackComment,
                    CallBackChooseService = data.CallBackChooseService
                };
                return View(dataViewModel);
                
        }

        // POST: CallBackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CallBackModel dataViewModel)
        {
            try
            {
                CallBack data = CallBackRepository.Find(id);

                data.CallBackId = dataViewModel.CallBackId;
                data.CallBackName = dataViewModel.CallBackName;
                data.CallBackEmail = dataViewModel.CallBackEmail;
                data.CallBackPhone = dataViewModel.CallBackPhone;
                data.CallBackComment = dataViewModel.CallBackComment;
                data.CallBackChooseService = dataViewModel.CallBackChooseService;
                data.EditDate = DateTime.UtcNow;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                CallBackRepository.Update(id,data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallBackController/Delete/5
        public ActionResult Delete(int id)
        {
            CallBackRepository.Delete(id, new CallBack()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
