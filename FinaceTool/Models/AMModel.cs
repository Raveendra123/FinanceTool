using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Models
{
    public class AMModel
    {
        [Required(ErrorMessage = "Please provide S_No")]
        public Nullable<long> S_No { get; set; }

        [Display(Name = "Customer Name")]
        public string Customername { get; set; }

        [Required(ErrorMessage = "Please select customer name")]
        public string CustomerId { get; set; }

        [Display(Name = "AM Name")]
        public string AMName { get; set; }

        [Required(ErrorMessage = "Please select AM name")]
        public string AMId { get; set; }

        [Required(ErrorMessage = "Please select lob Name")]

        public string LobId { get; set; }

        [Display(Name = "LOB Name")]

        public string LOBName { get; set; }
        public string opportunitylobname { get; set; }
        public string opportunitylobid { get; set; }

        [Display(Name = "DU Name")]
        public string DUName { get; set; }

        [Display(Name = "DBBLDU Name")]
        public string DBBLDU { get; set; }
        [Required(ErrorMessage = "Please select Du Name")]
        public string DuId { get; set; }
        public string DBBLDuId { get; set; }

        [Key]
        public int OpportunityKeyID { get; set; }

        public string OpportunityID { get; set; }

        [Required(ErrorMessage = "Please provide Opportunity Name")]
        [Display(Name = "Opportunity Name")]
        public string OpportunityName { get; set; }

        [Display(Name = "Service Line")]
        public string ServiceLine { get; set; }

        [Required(ErrorMessage = "Please select serviceline Name")]
        public string ServiceLineId { get; set; }
        [Required(ErrorMessage = "Please provide Program Name")]
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }

        [Display(Name = "Product Group")]
        public string ProductGroup { get; set; }

        [Required(ErrorMessage = "Please select product group name")]
        public string ProductGroupId { get; set; }

        [Display(Name = "TCV($K)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]

        [Required(ErrorMessage = "Please provide TCV value")]
        public Nullable<decimal> TCV { get; set; }

        [Display(Name = "ACV($K)")]

        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Required(ErrorMessage = "Please provide ACV value")]
        public Nullable<decimal> acv { get; set; }

        [Display(Name = "Sow Value($K)")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public Nullable<decimal> SowStatusValue { get; set; }


        [Display(Name = "Sow Status")]
        public Nullable<decimal> sowvalue { get; set; }

        public string SOWStatus
        {
            get;
            set;
        }

        [Display(Name = "Deal Stage")]
        public string DealStage { get; set; }

        [Required(ErrorMessage = "Please select deal stage name")]
        public string DealStageId { get; set; }

        public string OpportunityCategory { get; set; }

        [Required(ErrorMessage = "Please select sow status name")]
        public int SowStatusId { get; set; }

        [Required(ErrorMessage = "Please provide Billing StartDate")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Billing StartDate")]
        public Nullable<System.DateTime> BillingStratDate { get; set; }
        public string Note_Comment { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> lobList { get; set; }
        public List<SelectListItem> duList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public List<SelectListItem> serviceList { get; set; }
        public List<SelectListItem> DealStageList { get; set; }

        public List<SelectListItem> OpportunityCategoryList { get; set; }
        public List<SelectListItem> SowStatusList { get; set; }
        public List<SelectListItem> ProductGroupList { get; set; }
        public List<SelectListItem> AmUserList { get; set; }
        public string ProjectId { get; set; }
        //[Required(ErrorMessage = "Please Select Project Name")]
        public string ProjectIdForEdit { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public Nullable<decimal> AMJ19_FC { get; set; }
        public Nullable<decimal> JAS19_FC { get; set; }
        public Nullable<decimal> OND19_FC { get; set; }
        public string AMJ19_Text { get; set; }
        public string JAS19_Text { get; set; }
        public string OND19_Text { get; set; }
        public List<Quater> Quaterlist { get; set; }
        public bool IsUpdated { get; set; }
        
    }
}