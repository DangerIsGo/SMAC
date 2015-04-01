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
    
    public partial class School
    {
        public School()
        {
            this.Clubs = new HashSet<Club>();
            this.Grades = new HashSet<Grade>();
            this.LatestNews = new HashSet<LatestNews>();
            this.SchoolYears = new HashSet<SchoolYear>();
            this.Subjects = new HashSet<Subject>();
            this.TimeSlots = new HashSet<TimeSlot>();
            this.Users = new HashSet<User>();
        }
    
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    
        public virtual ICollection<Club> Clubs { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<LatestNews> LatestNews { get; set; }
        public virtual ICollection<SchoolYear> SchoolYears { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
