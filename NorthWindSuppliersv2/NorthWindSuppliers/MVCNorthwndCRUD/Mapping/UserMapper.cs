using DataLayer.Models;
using MVCNorthwndCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCNorthwndCRUD.Mapping
{
    public static class UserMapper
    {
        public static UserPO MapDoToPo(UserDO from)
        {
            UserPO to = new UserPO();
            to.UserID = from.UserID;
            to.Username = from.Username;
            to.Password = from.Password;
            to.Firstname = from.Firstname;
            to.Lastname = from.Lastname;
            to.EmailAddress = from.EmailAddress;
            to.RoleID = from.RoleID;

            return to;
        }

        public static UserDO MapPoToDo(UserPO from)
        {
            UserDO to = new UserDO();
            to.UserID = from.UserID;
            to.Username = from.Username;
            to.Password = from.Password;
            to.Firstname = from.Firstname;
            to.Lastname = from.Lastname;
            to.EmailAddress = from.EmailAddress;
            to.RoleID = from.RoleID;

            return to;
        }
    }
}