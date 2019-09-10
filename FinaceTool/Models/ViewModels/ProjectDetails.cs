using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaceTool.Models.ViewModels
{
    public class ProjectDetails
    {
        public long? SNo { get; set; }
        public String CustomerName { get; set; }
        public string AMName { get; set; }
        public string LOB { get; set; }
        public string DUName { get; set; }
        public string DBBLDU { get; set; }
        public int OpportunityID{ get; set; }
        public string OpportunityName { get; set; }
        public int ProjectID { get; set; }
        public string ProgramName { get; set; }
        public string ServiceLine { get; set; }
        public string ProductGroup { get; set; }
        public decimal? TCV { get; set;}
        public decimal? ACV { get; set; }
        public decimal? SOWValue { get; set; }
        public string DealStage { get; set; }
        public DateTime? BillingStartDate { get; set; }
        public string Note { get; set; }
        public int AMJ19_FC { get; set; }
        public int JAS19_FC { get; set; }
        public int OND19_FC { get; set; }
        public int JFM20_FC { get; set; }
        public int AMJ20_FC { get; set; }
        public int JAS20_FC { get; set; }
        public int OND20_FC { get; set; }
        public int JFM21_FC { get; set; }
        public int AMJ21_FC { get; set; }
        public List<string> CustomerNames { get; set; }
        public List<string> AMNames { get; set; }
        public List<string> LOBs { get; set; }
        public List<string> DUNames { get; set; }
        public List<string> DBBLDUs { get; set; }
        public List<int> OppurtunityIDs { get; set; }
        public List<string> OpportunityNames { get; set; }
        public List<int> ProjectIDs { get; set; }
        public List<string> ProgramNames { get; set; }
        public List<string> ServiceLines { get; set; }
        public List<string> ProductGroups { get; set; }
    }
}