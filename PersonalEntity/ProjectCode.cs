//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonalEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectCode()
        {
            this.SubCodes = new HashSet<ProjectSubCode>();
            this.BilledTimes = new HashSet<BilledTime>();
        }
    
        public int ProjectCodeID { get; set; }
        public string ProjectCodeValue { get; set; }
        public string ProjectCodeDescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectSubCode> SubCodes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BilledTime> BilledTimes { get; set; }
    }
}
