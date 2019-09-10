using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Models
{
    public class DUHMainDetailsResult
    {
        public Nullable<long> S_No { get; set; }

        [Display(Name = "Customer Name")]
        public string Customername { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string AMName { get; set; }
        public Nullable<int> AMID { get; set; }
        public string LOBName { get; set; }
        public Nullable<int> LOBID { get; set; }
        public Nullable<int> SDUID { get; set; }
        public string DUName { get; set; }
        public Nullable<int> DUID { get; set; }
        public string DBBLDU { get; set; }
        public Nullable<int> DBBLDUID { get; set; }
     
        public Nullable<int> OpportunityKeyID { get; set; }
        public Nullable<int> OpportunityID { get; set; }
        public string OpportunityName { get; set; }
        public Nullable<long> ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ServiceLine { get; set; }
        public string ProgramName { get; set; }
        public Nullable<int> ServiceLineID { get; set; }
        public string ProductGroup { get; set; }
        public Nullable<int> ProductGroupID { get; set; }

       
        public string SowStatus { get; set; }

        [Display(Name = "POAvilable")]
        
        public string poavilable { get; set; }

        [Display(Name = "POBalance($K)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public string pobalance { get; set; }
        //public Nullable<int> AMJ19_FC { get; set; }
        //public Nullable<int> JAS19_FC { get; set; }
        //public Nullable<int> OND19_FC { get; set; }
        //public Nullable<int> AMJ19_ACT { get; set; }
        //public Nullable<int> JAS19_ACT { get; set; }
        //public Nullable<int> OND19_ACT { get; set; }
        public string Note { get; set; }
        public Nullable<int> OpportunityLobId { get; set; }
        public string OpportunityLobName { get; set; }
        public Nullable<int> DealStageID { get; set; }

        public List<string> Quaterlist { get; set; }
        public List<string> QuaterName { get; set; }
        public List<SelectListItem> SDUList { get; set; }

        public List<SelectListItem> DUList { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CompareDate { get; set; }
    }
}