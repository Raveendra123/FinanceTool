using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaceTool.Models
{
    public class UserTemp
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public Nullable<bool> ISActive { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Password { get; set; }

        public int RoleName { get; set; }
    }
}