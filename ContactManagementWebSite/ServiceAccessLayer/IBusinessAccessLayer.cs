using ContactManagementWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManagementWebSite.ServiceAccessLayer
{
    public interface IBusinessAccessLayer
    {
        List<Contact> GetAllContacts();

        Contact GetContactById(int id);

        bool AddContact(Contact contact);

        bool UpdateContact(Contact contact);

        bool DeleteContact(int id);


    }
}