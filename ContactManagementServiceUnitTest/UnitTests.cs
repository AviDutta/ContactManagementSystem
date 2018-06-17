using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactManagementWebSite.Controllers;
using ContactManagementWebSite.Models;
using ContactManagementWebSite.ServiceAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ContactManagementWebSiteUnitTest
{
    [TestClass]
    public class UnitTests
    {
        private ContactController contactController;
        private IBusinessAccessLayer serviceAccessLayer;

        public UnitTests()
        {
            contactController = new ContactController();
            serviceAccessLayer = new BusinessAccessLayer();
        }

        [TestMethod]
        public void TC_VerifyContactCreated()
        {
            Random generator = new Random();
            string firstname = generator.Next(0, 1000000).ToString("D6");
            var inputContact = new Contact()
            {
                FirstName = firstname,
                LastName = "Test",
                Email = "testUSer@doamian.com",
                PhoneNumber = "9087654321",
                Status = 1
            };

            //Create contact
            contactController.CreatePost(inputContact);
            List<Contact> lstContactsFromAPI = serviceAccessLayer.GetAllContacts();

            Assert.IsTrue(lstContactsFromAPI.Where(C => C.FirstName == inputContact.FirstName).Any());
        }

        [TestMethod]
        public void TC_VerifyContactUpdated()
        {
            Random generator = new Random();
            string firstname = generator.Next(0, 1000000).ToString("D6");
            var inputContact = new Contact()
            {
                FirstName = firstname,
                LastName = "Test",
                Email = "testUSer@doamian.com",
                PhoneNumber = "9087654321",
                Status = 1
            };

            contactController.CreatePost(inputContact);
            List<Contact> lstContactsFromAPI = serviceAccessLayer.GetAllContacts();
            Assert.IsTrue(lstContactsFromAPI.Where(C => C.FirstName == inputContact.FirstName).Any());

            Contact updateContact = lstContactsFromAPI.Where(C => C.FirstName == inputContact.FirstName).FirstOrDefault();
            firstname = generator.Next(0, 1000000).ToString("D6");

            updateContact.FirstName = firstname;
            updateContact.LastName = "TestUpdate";
            updateContact.Email = "updatetestUSer@doamian.com";
            updateContact.PhoneNumber = "9087654567";
            updateContact.Status = 0;

            //Put - Update contact
            contactController.EditPut(updateContact);
            List<Contact> lstUpdateContactsFromAPI = serviceAccessLayer.GetAllContacts();
            Assert.IsTrue(lstUpdateContactsFromAPI.Where(C => C.FirstName == updateContact.FirstName).Any());

        }

        [TestMethod]
        public void TC_VerifyContactGetByContactId()
        {
            Random generator = new Random();
            string firstname = generator.Next(0, 1000000).ToString("D6");
            var inputContact = new Contact()
            {
                FirstName = firstname,
                LastName = "Test",
                Email = "testUSer@doamian.com",
                PhoneNumber = "9087654321",
                Status = 1
            };

            contactController.CreatePost(inputContact);
            List<Contact> lstContactsFromAPI = serviceAccessLayer.GetAllContacts();
            Assert.IsTrue(lstContactsFromAPI.Where(C => C.FirstName == inputContact.FirstName).Any());
            Contact updateContact = lstContactsFromAPI.Where(C => C.FirstName == inputContact.FirstName).FirstOrDefault();
            //Get contact by contactID
            Contact lstUpdateContact = serviceAccessLayer.GetContactById(updateContact.Id);
            Assert.IsNotNull(lstUpdateContact);
        }

        [TestMethod]
        public void TC_VerifyGetAllContact()
        {
            Random generator = new Random();
            string firstname = generator.Next(0, 1000000).ToString("D6");
            var inputContact = new Contact()
            {
                FirstName = firstname,
                LastName = "Test",
                Email = "testUSer@doamian.com",
                PhoneNumber = "9087654321",
                Status = 1
            };

            contactController.CreatePost(inputContact);
            //Get all Contacts
            List<Contact> lstContactsFromAPI = serviceAccessLayer.GetAllContacts();
            Assert.IsTrue(lstContactsFromAPI.Count > 0);

        }

        [TestMethod]
        public void TC_VerifyContactDeletedByContactId()
        {
            Random generator = new Random();
            string firstname = generator.Next(0, 1000000).ToString("D6");
            var inputContact = new Contact()
            {
                FirstName = firstname,
                LastName = "Test",
                Email = "testUSer@doamian.com",
                PhoneNumber = "9087654321",
                Status = 1
            };

            contactController.CreatePost(inputContact);
            List<Contact> lstContactsFromAPI = serviceAccessLayer.GetAllContacts();
            Assert.IsTrue(lstContactsFromAPI.Where(C => C.FirstName == inputContact.FirstName).Any());

            Contact updateContact = lstContactsFromAPI.Where(C => C.FirstName == inputContact.FirstName).FirstOrDefault();
            //Delete contact by contactId
            serviceAccessLayer.DeleteContact(updateContact.Id);
            Contact lstdeletedContact = serviceAccessLayer.GetContactById(updateContact.Id);
            Assert.IsNull(lstdeletedContact);
        }

        [TestMethod]
        public void TC_VerifyGetContactUsingInvalidContactId()
        {
            Random generator = new Random();
            int randomNumber = generator.Next(0, 1000000);
            Contact contact = serviceAccessLayer.GetContactById(randomNumber);
            Assert.IsNull(contact);
        }

        [TestMethod]
        public void TC_VerifyDeleteContactUsingInvalidContactId()
        {
            Random generator = new Random();
            int randomNumber = generator.Next(0, 1000000);
            bool status = serviceAccessLayer.DeleteContact(randomNumber);
            Assert.IsTrue(!status);
        }

        [TestMethod]
        public void TC_VerifyContactCreationUsingInvalidData()
        {
            Random generator = new Random();
            string firstname = generator.Next(0, 1000000).ToString("D6");
            var inputContact = new Contact()
            {
                FirstName = firstname,
                LastName = "Test@#$",
                Email = "test@$USer@doamian.com",
                PhoneNumber = "90#$%^#^654321",
                Status = 1
            };

            //Create contact
            contactController.CreatePost(inputContact);
            Assert.IsTrue(!contactController.ModelState.IsValid);
        }

        [TestMethod]
        public void TC_VerifyContactUpdateUsingInvalidData()
        {
            Random generator = new Random();
            string firstname = generator.Next(0, 1000000).ToString("D6");
            var inputContact = new Contact()
            {
                FirstName = firstname,
                LastName = "Test@#$",
                Email = "test@$USer@doamian.com",
                PhoneNumber = "90#$%^#^654321",
                Status = 1
            };

            //update contact
            contactController.EditPut(inputContact);
            Assert.IsTrue(!contactController.ModelState.IsValid);
        }

        [TestMethod]
        public void TC_VerifyContactCreationWithoutRequiredFields()
        {
            Random generator = new Random();
            string firstname = generator.Next(0, 1000000).ToString("D6");
            var inputContact = new Contact()
            {
                //FirstName = firstname,
                LastName = "Test",
                //Email = "testUSer@doamian.com",
                //PhoneNumber = "9087654321",
                //Status = 1
            };

            //Create contact
            contactController.CreatePost(inputContact);
            Assert.IsTrue(!contactController.ModelState.IsValid);
        }

    }
}
