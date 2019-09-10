using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinaceTool.Models
{
    public class AMFilterPidModel
    {
        public string ProjectName { get; set; }
        public string ProgramName { get; set; }
        public string ServiceLineName { get; set; }
        public string ProductGroupName { get; set; }
        public string SOWStatus { get; set; }
        public string DealStage { get; set; }
        public string description { get; set; }
    }
}