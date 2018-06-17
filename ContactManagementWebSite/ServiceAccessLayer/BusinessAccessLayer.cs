using ContactManagementWebSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.ModelBinding;

namespace ContactManagementWebSite.ServiceAccessLayer
{
    public class BusinessAccessLayer : IBusinessAccessLayer
    {
        private string apiBaseUri;

        public BusinessAccessLayer()
        {
            this.apiBaseUri = ConfigurationManager.AppSettings["WebAPIBaseURL"];
        }

        //Get All Contacts from the API GET call
        public List<Contact> GetAllContacts()
        {
            List<Contact> contacts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUri);
                //HTTP GET
                var responseTask = client.GetAsync("contact");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Contact>>(); ;
                    readTask.Wait();

                    contacts = readTask.Result;
                }

                return contacts;
            }
        }

        public bool AddContact(Contact contact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUri);
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Contact>("contact", contact);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public Contact GetContactById(int id)
        {
            Contact contact = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUri);
                //HTTP GET
                var responseTask = client.GetAsync("contact?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Contact>();
                    readTask.Wait();

                    contact = readTask.Result;
                }
                return contact;
            }
        }

        public bool UpdateContact(Contact contact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(string.Concat(apiBaseUri, "contact"));
                //HTTP POST
                var postTask = client.PutAsJsonAsync<Contact>("contact", contact);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }

        public bool DeleteContact(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUri);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("contact/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
        }


    }
}