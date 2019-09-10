using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinaceTool.Models
{
    public class AdminMainDetailsResult
    {
        public Nullable<long> S_No { get; set; }
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
        public Nullable<int> ProjectID { get; set; }
        public string ServiceLine { get; set; }
        public string ProgramName { get; set; }
        public Nullable<int> ServiceLineID { get; set; }
        public string ProductGroup { get; set; }
        public Nullable<int> ProductGroupID { get; set; }

        [Display(Name = "TCV($K)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public Nullable<decimal> TCV { get; set; }

        [Display(Name = "ACV($K)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public Nullable<decimal> acv { get; set; }

        [Display(Name = "SowStatus")]
        public string sowvalue { get; set; }
        public Nullable<int> DealStage { get; set; }
        public Nullable<System.DateTime> BillingStratDate { get; set; }
        public string note { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<decimal> AMJ19 { get; set; }
        public Nullable<decimal> JAS19 { get; set; }
        public Nullable<decimal> OND19 { get; set; }
        public Nullable<int> OpportunityLobId { get; set; }
        public string OpportunityLobName { get; set; }

        [Display(Name = "SowStatusValue($K)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public Nullable<decimal> SowStatusValue { get; set; }
        public List<string> Quaterlist { get; set; }
        public List<string> QuaterName { get; set; }
        public bool IsUpdated { get; set; }
        public string DealStagestatus { get; set; }

        
    }
}