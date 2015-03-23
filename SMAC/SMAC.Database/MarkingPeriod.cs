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
    
    public partial class MarkingPeriod
    {
        public MarkingPeriod()
        {
            this.Enrollments = new HashSet<Enrollment>();
            this.TeacherSchedules = new HashSet<TeacherSchedule>();
        }
    
        public int MarkingPeriodId { get; set; }
        public int SchoolId { get; set; }
        public string Period { get; set; }
        public bool FullYear { get; set; }
    
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<TeacherSchedule> TeacherSchedules { get; set; }
    }
}