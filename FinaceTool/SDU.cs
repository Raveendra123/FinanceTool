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
    
    public partial class SDU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SDU()
        {
            this.DUs = new HashSet<DU>();
        }
    
        public int SDUID { get; set; }
        public string SDUName { get; set; }
        public int LOBID { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DU> DUs { get; set; }
        public virtual LOB LOB { get; set; }
    }
}
