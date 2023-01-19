using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Constants
{
    public static class Roles
    {
        public static List<string> All = new List<string>
        {
            Admin,
            SuperAdmin,
            User
        };
        public const string User = "User";
        public const string Admin = "Admin";
        public const string SuperAdmin = "Super Admin";

    }
}
