//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinaceTool
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectForecastByDUH
    {
        public int DUhforecastid { get; set; }
        public int OpportunityID { get; set; }
        public Nullable<decimal> forecastvaluebyduh { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> QuaterID { get; set; }
        public bool IsNew { get; set; }
    
        public virtual Opportunity Opportunity { get; set; }
        public virtual Quater Quater { get; set; }
    }
}
