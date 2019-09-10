using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaceTool.Models
{
    public class ProjectTemp
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int OpportunityID { get; set; }
        public int SowStatusID { get; set; }
        public string poavilable { get; set; }
        public string pobalance { get; set; }
        public string Note { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public int OpportunityName { get; set; }

        public int SOWStatus { get; set; }

        public string ProjectCode { get; set; }



    }
}