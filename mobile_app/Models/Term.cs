using System;
using SQLite;

namespace mobile_app.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }// Primary Key
        public int CourseId { get; set; }// Foreign key from Course class/table
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
