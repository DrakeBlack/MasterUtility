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
    
    public partial class WorkflowNote
    {
        public int WorkflowNoteID { get; set; }
        public Nullable<int> WorkflowID { get; set; }
        public Nullable<int> WorkflowStepID { get; set; }
        public Nullable<int> WorkflowBugID { get; set; }
        public string WorkflowDescription { get; set; }
    
        public virtual Workflow Workflow { get; set; }
        public virtual WorkflowBug Bug { get; set; }
        public virtual WorkflowStep Step { get; set; }
    }
}
