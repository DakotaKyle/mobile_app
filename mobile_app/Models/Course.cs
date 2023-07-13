using System;
using SQLite;

namespace mobile_app.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TermId { get; set; } // Foreign key for terms
        public string Name { get; set; }
        public String Status { get; set; }
        public string InstructorName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
