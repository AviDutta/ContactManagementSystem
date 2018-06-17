using ContactManagementWebSite.Extensions;
using ContactManagementWebSite.Models;
using ContactManagementWebSite.ServiceAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactManagementWebSite.Controllers
{
    public class ContactController : Controller
    {
        private IBusinessAccessLayer serviceAccessLayer;

        public ContactController()
        {
            this.serviceAccessLayer = new BusinessAccessLayer();
        }

        // GET: /Contact/
        [HttpGet]
        public ActionResult Index()
        {

            IList<Contact> lstContactsFromAPI = serviceAccessLayer.GetAllContacts();
            return View(lstContactsFromAPI);
        }


        // GET: /Contact/1
        [HttpGet]
        public ActionResult Details(int id)
        {
            Contact contact = serviceAccessLayer.GetContactById(id);
            if (contact == null)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return View();
            }
            return View(contact);
        }


        //GET: /Contact/
        [HttpGet]
        [ActionName("Create")]
        public ActionResult CreateGet()
        {

            return View();
        }

        //POST: /Contact/
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost([Bind(Exclude = "Id")]Contact contact)
        {
            bool isAdded;
            if (ModelState.IsValid)
            {
                isAdded = serviceAccessLayer.AddContact(contact);
                if (isAdded)
                    return Content("<script language='javascript' type='text/javascript'>alert('Contact details added successfully.');window.location.href = '/Contact/Index';</script>");

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(contact);
        }


        // GET: /Contact/1
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditGet(int id)
        {
            Contact contact = serviceAccessLayer.GetContactById(id);
            if (contact == null)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return View();
            }
            return View(contact);
        }


        //POST: /Contact/
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPut(Contact contact)
        {
            bool isUpdated;
            if (ModelState.IsValid)
            {
                isUpdated = serviceAccessLayer.UpdateContact(contact);
                if (isUpdated)
                    return Content("<script language='javascript' type='text/javascript'>alert('Contact details updated successfully.');window.location.href = '/Contact/Index';</script>");

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(contact);
        }

        // Delete: /Contact/1
        public ActionResult Delete(int id)
        {
            bool isDeleted = serviceAccessLayer.DeleteContact(id);
            if (isDeleted)
                return Content("<script language='javascript' type='text/javascript'>alert('Contact details deleted successfully.');window.location.href = '/Contact/Index';</script>");

            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

            return View();
        }

    }
}