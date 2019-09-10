using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Models
{
    public class AMDetails
    {
        public Nullable<long> S_No { get; set; }
        public string Customername { get; set; }

        public string CustomerId { get; set; }
        public string AMName { get; set; }
        public string LobId { get; set; }
        public string LOBName { get; set; }
        public string DUName { get; set; }
        public string DBBLDU { get; set; }

        [Key]
        public int OpportunityKeyID { get; set; }
        public int OpportunityID { get; set; }
        public string OpportunityName { get; set; }
        public string ServiceLine { get; set; }
        public string ProgramName { get; set; }
        public string ProductGroup { get; set; }
        public Nullable<decimal> TCV { get; set; }
        public Nullable<decimal> acv { get; set; }
        public Nullable<decimal> sowvalue { get; set; }
        public string DealStage { get; set; }
        public Nullable<System.DateTime> BillingStratDate { get; set; }
        public string Note_Comment { get; set; }
        public Nullable<decimal> AMJ19_FC { get; set; }
        public  List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> lobList { get; set; }
        public List<SelectListItem> duList { get; set; }
        public List<SelectListItem> projectList { get; set; }
    }
}