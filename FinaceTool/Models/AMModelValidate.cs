using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FinaceTool.Models
    {
        public class AMModelValidate
        {
        [Required]
        public Nullable<long> S_No { get; set; }
        public string Customername { get; set; }

        [Required(ErrorMessage = "Please Select Customer Name")]
        public string CustomerId { get; set; }
        public string AMName { get; set; }

        [Required(ErrorMessage = "Please Select AM Name")]
        public string AMId { get; set; }

        [Required(ErrorMessage = "Please Select Lob Name")]
        public string LobId { get; set; }
        public string LOBName { get; set; }
        // public string opportunitylobname { get; set; }
        //  public string opportunitylobid { get; set; }

        public string DUName { get; set; }
        public string DBBLDU { get; set; }

        [Required(ErrorMessage = "Please Select Lob Name")]
        public string DuId { get; set; }
        //public string DBBLDuId { get; set; }

        [Key]
        public int OpportunityKeyID { get; set; }
        //public string OpportunityID { get; set; }

        //[Required(ErrorMessage = "Please provide a OpportunityName")]
        //public string OpportunityName { get; set; }
        //public string ServiceLine { get; set; }
        //public string ServiceLineId { get; set; }
        //public string ProgramName { get; set; }
        //public string ProductGroup { get; set; }

        //public string ProductGroupId { get; set; }

        //public Nullable<decimal> TCV { get; set; }
        //public Nullable<decimal> acv { get; set; }
        //public Nullable<decimal> sowvalue { get; set; }
        //public string DealStage { get; set; }

        //public string DealStageId { get; set; }

        //public int SowStatusId { get; set; }
        //public Nullable<System.DateTime> BillingStratDate { get; set; }
        //public string Note_Comment { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> lobList { get; set; }
        public List<SelectListItem> duList { get; set; }
        //public List<SelectListItem> ProjectList { get; set; }
        //public List<SelectListItem> serviceList { get; set; }
        //public List<SelectListItem> DealStageList { get; set; }
        //public List<SelectListItem> SowStatusList { get; set; }
        //public List<SelectListItem> ProductGroupList { get; set; }
        public List<SelectListItem> AmUserList { get; set; }
        //public string ProjectId { get; set; }
        //public string ProjectName { get; set; }
        //public Nullable<decimal> AMJ19_FC { get; set; }
        //public Nullable<decimal> JAS19_FC { get; set; }
        //public Nullable<decimal> OND19_FC { get; set; }
        //public string AMJ19_Text { get; set; }
        //public string JAS19_Text { get; set; }
        //public string OND19_Text { get; set; }
    }
}