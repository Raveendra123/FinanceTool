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
    
    public partial class Opportunity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Opportunity()
        {
            this.Projects = new HashSet<Project>();
            this.ProjectActualByDUHs = new HashSet<ProjectActualByDUH>();
            this.ProjectForecastByDUHs = new HashSet<ProjectForecastByDUH>();
        }
    
        public int OpportunityID { get; set; }
        public string OpportunityName { get; set; }
        public int DealStageID { get; set; }
        public int SowStatusID { get; set; }
        public int AMID { get; set; }
        public int CustomerID { get; set; }
        public string ProgramName { get; set; }
        public int ServiceLineID { get; set; }
        public int ProductGroupID { get; set; }
        public int DBBLDUID { get; set; }
        public Nullable<decimal> TCV { get; set; }
        public Nullable<decimal> acv { get; set; }
        public Nullable<int> sowvalue { get; set; }
        public Nullable<System.DateTime> BillingStratDate { get; set; }
        public string Note { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int SalesForceCastID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> SalesForceGuid { get; set; }
        public Nullable<int> DUID { get; set; }
        public Nullable<int> LobId { get; set; }
        public Nullable<decimal> SowStatusValue { get; set; }
        public Nullable<int> Userid { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<bool> IsMapped { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDUHUpdated { get; set; }
        public string OpportunityCategory { get; set; }
    
        public virtual LOB LOB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectActualByDUH> ProjectActualByDUHs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectForecastByDUH> ProjectForecastByDUHs { get; set; }
    }
}