using System;
using System.Collections.Generic;
using Authorizeniki.Datalayer.Tables;

namespace Authorizeniki.Models
{
    public class UserModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public string SelectedRoleId { get; set; }
    }
}