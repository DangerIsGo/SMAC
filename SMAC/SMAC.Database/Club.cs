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
    
    public partial class Club
    {
        public Club()
        {
            this.ClubEnrollments = new HashSet<ClubEnrollment>();
            this.ClubSchedules = new HashSet<ClubSchedule>();
        }
    
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int SchoolId { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<ClubEnrollment> ClubEnrollments { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<ClubSchedule> ClubSchedules { get; set; }
    }
}
