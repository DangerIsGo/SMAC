//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMAC.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class PrivateMessage
    {
        public int PrivateMessageId { get; set; }
        public string ToUser { get; set; }
        public string FromUser { get; set; }
        public string Content { get; set; }
        public System.DateTime DateSent { get; set; }
        public Nullable<System.DateTime> DateRead { get; set; }
    
        public virtual User UserSentFrom { get; set; }
        public virtual User UserSentTo { get; set; }
    }
}