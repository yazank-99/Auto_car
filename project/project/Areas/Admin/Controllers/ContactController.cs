using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Models.Repository;
using project.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        public IRepository<Contact> ContactRepository { get; }

        public ContactController(IRepository<Contact> ContactRepository)
        {
            this.ContactRepository = ContactRepository;
        }
        // GET: ContactController
        public ActionResult Index()
        {
            IList<Contact> dataList = ContactRepository.View();
            IList<ContactModel> dataViewModelList = new List<ContactModel>();
            foreach (var data in dataList)
            {
                ContactModel dataViewModel = new ContactModel()
                {
                    ContactId= data.ContactId,
                    ContactEmail= data.ContactEmail,
                    ContactName= data.ContactName,
                    ContactQuestion = data.ContactQuestion,
                    ContactSubject = data.ContactSubject,
                    IsActive=data.IsActive

                };
                dataViewModelList.Add(dataViewModel);
            }
            return View(dataViewModelList);
        }

        public ActionResult Active(int id)
        {
            ContactRepository.Active(id, new Contact()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }

        // GET: ContactController/Create
        public ActionResult Create()
        {
            ContactModel dataViewModel = new ContactModel();
            return View(dataViewModel);
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactModel dataViewModel)
        {
            try
            {

                Contact data = new Contact()
                {
                    ContactId=dataViewModel.ContactId,
                    ContactEmail = dataViewModel.ContactEmail,
                    ContactName=dataViewModel.ContactName,
                    ContactQuestion=dataViewModel.ContactQuestion,
                    ContactSubject=dataViewModel.ContactSubject,
                    IsActive = true,
                    IsDelete = false,

                };
                ContactRepository.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactController/Edit/5
        public ActionResult Edit(int id)
        {
            Contact data = ContactRepository.Find(id);
            ContactModel dataViewModel = new ContactModel()
            {
               ContactId = data.ContactId,
               ContactSubject = data.ContactSubject,
               ContactName=data.ContactName,
               ContactEmail=data.ContactEmail,
               ContactQuestion = data.ContactQuestion
            };
            return View(dataViewModel);
        }

        // POST: ContactController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ContactModel dataViewModel)
        {
            try
            {
                Contact data = ContactRepository.Find(id);
                data.ContactId = dataViewModel.ContactId;
                data.ContactName = dataViewModel.ContactName;
                data.ContactEmail = dataViewModel.ContactEmail;
                data.ContactQuestion = dataViewModel.ContactQuestion;
                data.ContactSubject = dataViewModel.ContactSubject;
                data.EditDate = DateTime.UtcNow;
                data.EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ContactRepository.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactController/Delete/5
        public ActionResult Delete(int id)
        {
            ContactRepository.Delete(id, new Contact()
            {
                EditDate = DateTime.UtcNow,
                EditUser = User.FindFirstValue(ClaimTypes.NameIdentifier)
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
