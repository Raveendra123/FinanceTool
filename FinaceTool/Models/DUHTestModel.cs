using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Models
{
    public class DUHTestModel
    {
        public long S_No { get; set; }
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
        public string AMName { get; set; }
        public string LOBName { get; set; }
        public string DUName { get; set; }
        public string DBBLDU { get; set; }
        public Nullable<int> OpportunityID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProgramName { get; set; }
        public string ServiceLine { get; set; }
        public string ProductGroup { get; set; }
        public Nullable<decimal> sowvalue { get; set; }
        public Nullable<bool> poavailable { get; set; }
        public Nullable<decimal> pobalance { get; set; }
        public Nullable<int> AMJ19_FC { get; set; }
        public Nullable<int> AMJ19_Act { get; set; }
        public List<SelectListItem> DUHCustomerList { get; set; }
        public List<SelectListItem> DUHlobList { get; set; }
        public List<SelectListItem> DUHduList { get; set; }
        public List<SelectListItem> DUHprojectList { get; set; }
        public List<SelectListItem> serviceList { get; set; }
        public List<SelectListItem> DealStageList { get; set; }
        public List<SelectListItem> SowStatusList { get; set; }
        public List<SelectListItem> ProductGroupList { get; set; }
        public List<SelectListItem> AmUserList { get; set; }
    }
}
