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
        public string Color { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
