using ContactManagementServices.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactManagementServices.Controllers
{
    public class ContactController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllContacts()
        {
            IList<Contact> listContacts = null;
            using (var ctx = new ContactsDBEntities())
            {
                listContacts = ctx.Contacts.ToList();
            }
            if (listContacts.Count() == 0)
                return NotFound();

            return Ok(listContacts);
        }

        [HttpGet]
        public IHttpActionResult GetContactById(int id)
        {
            Contact contact = null;
            using (var ctx = new ContactsDBEntities())
            {
                contact = ctx.Contacts.Where(c => c.ID == id).FirstOrDefault();
                if (contact == null)
                    return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public IHttpActionResult PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model validation failed.");

            using (var ctx = new ContactsDBEntities())
            {
                ctx.Contacts.Add(contact);
                ctx.SaveChanges();
            }
            return Ok();
        }

        
        public IHttpActionResult PutContact(Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new ContactsDBEntities())
            {
                var existingContact = ctx.Contacts.Where(s => s.ID == contact.ID).FirstOrDefault();

                if (existingContact != null)
                {
                    existingContact.FirstName = contact.FirstName;
                    existingContact.LastName = contact.LastName;
                    existingContact.Email = contact.Email;
                    existingContact.PhoneNumber = contact.PhoneNumber;
                    existingContact.Status = contact.Status;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = null;
            using (var ctx = new ContactsDBEntities())
            {
                contact = ctx.Contacts.Find(id);
                if (contact == null)
                    return NotFound();

                ctx.Contacts.Remove(contact);
                ctx.SaveChanges();
            }
            return Ok(contact);
        }

    }
}
