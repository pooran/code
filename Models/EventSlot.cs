//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalBuild.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventSlot
    {
        public System.Guid Id { get; set; }
        public System.Guid EventId { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public System.DateTime ModifiedTime { get; set; }
        public Nullable<System.Guid> BookedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Event Event { get; set; }
    }
}