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
    
    public partial class Leave
    {
        public int LeaveID { get; set; }
        public int LeaveTypeID { get; set; }
        public System.DateTime LeaveDate { get; set; }
        public double LeaveHours { get; set; }
    
        public virtual LeaveType LeaveType { get; set; }
    }
}
