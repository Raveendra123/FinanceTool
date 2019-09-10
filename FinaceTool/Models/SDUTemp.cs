using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaceTool.Models
{
    public class SDUTemp
    {
        public int SDUID { get; set; }
        public string SDUName { get; set; }
        public int LOBID { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public int LOBName { get; set; }
    }
}