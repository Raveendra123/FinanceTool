using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaceTool.Models
{
    public class DUTemp
    {
        public int DUID { get; set; }
        public int AMID { get; set; }
        public string DUName { get; set; }
        public int SDUID { get; set; }
        public int DUHeadID { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int AMName { get; set; }
        public int SDUName { get; set; }

        public string DUHeadName { get; set; }
    }
}