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
    
    public partial class TeacherSchedule
    {
        public string UserId { get; set; }
        public string SectionName { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public int SchoolId { get; set; }
        public int MarkingPeriodId { get; set; }
        public string SchoolYear { get; set; }
    
        public virtual MarkingPeriod MarkingPeriod { get; set; }
        public virtual SchoolYear SchoolYear1 { get; set; }
        public virtual Section Section { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
