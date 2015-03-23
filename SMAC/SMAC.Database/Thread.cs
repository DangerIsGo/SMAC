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
    
    public partial class Thread
    {
        public Thread()
        {
            this.Threads1 = new HashSet<Thread>();
            this.KhanShares = new HashSet<KhanShare>();
        }
    
        public int ThreadId { get; set; }
        public string UserId { get; set; }
        public string SectionName { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public int SchoolId { get; set; }
        public string ThreadTitle { get; set; }
        public string Content { get; set; }
        public System.DateTime DateTimePosted { get; set; }
        public Nullable<int> RepliedTo { get; set; }
    
        public virtual Section Section { get; set; }
        public virtual ICollection<Thread> Threads1 { get; set; }
        public virtual Thread Thread1 { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<KhanShare> KhanShares { get; set; }
    }
}