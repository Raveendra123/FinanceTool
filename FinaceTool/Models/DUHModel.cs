using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinaceTool.Models
{
    public class DUHModel 
    {
        [Required(ErrorMessage = "Please provide S_No")]
        public Nullable<long> S_No { get; set; }
        public string Customername { get; set; }
        [Required(ErrorMessage = "Please select customer name")]
        public Nullable<int> CustomerID { get; set; }
        public string AMName { get; set; }
        [Required(ErrorMessage = "Please select AM name")]
        public Nullable<int> AMID { get; set; }
        public string LOBName { get; set; }
        [Required(ErrorMessage = "Please select lob Name")]
        public int LOBID { get; set; }
        public int SDUID { get; set; }
        public string DUName { get; set; }
        [Required(ErrorMessage = "Please select Du Name")]
        public Nullable<int> DUID { get; set; }
        public string DBBLDU { get; set; }
        public Nullable<int> DBBLDUID { get; set; }
        [Key]
        public Nullable<int> OpportunityKeyID { get; set; }
        public Nullable<int> OpportunityID { get; set; }
        [Required(ErrorMessage = "Please provide Opportunity Name")]
        public string OpportunityName { get; set; }

        [Required(ErrorMessage = "Please select Project Name")]
        public Nullable<long> ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ServiceLine { get; set; }

        [Required(ErrorMessage = "Please select Program Name")]
        public string ProgramName { get; set; }

        [Required(ErrorMessage = "Please select Serviceline Name")]
        public Nullable<int> ServiceLineID { get; set; }
        public string ProductGroup { get; set; }

        [Required(ErrorMessage = "Please select Product Group Name")]
        public Nullable<int> ProductGroupID { get; set; }

        [Required(ErrorMessage = "Please select Sow Status")]
        public Nullable<decimal> SowStatus { get; set; }

        [Required(ErrorMessage = "Please select poavailable Status")]
        public string poavilable { get; set; }
        public string pobalance { get; set; }
        public string Note { get; set; }
        public string dealstageId { get; set; }
        public List<SelectListItem> lobList { get; set; }
        public List<SelectListItem> duList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public List<SelectListItem> serviceList { get; set; }
        public List<SelectListItem> DealStageList { get; set; }
        public List<SelectListItem> SowStatusList { get; set; }
        public List<SelectListItem> ProductGroupList { get; set; }
        public List<SelectListItem> AmUserList { get; set; }
        public List<SelectListItem> OpportunityList { get; set; }
        public IEnumerable<FinaceTool.Models.CustomerDropdown> CustomerList { get; set; }
        public Nullable<int> selectedcustomerId { get; set; }
        public string selectedcustomerText { get; set; }
        public string customertest { get; set; }
        public List<SelectListItem> poAvailablelist { get; set; }
        public string OND19_Text { get; set; }
        public List<Quater> Quaterlist { get; set; }

        public List<string> QuaterListData { get; set; }
        public List<SelectListItem> SDUList { get; set; }

        public List<SelectListItem> DUList { get; set; }
       
       


        public DateTime CompareDate { get; set; }
    }
}