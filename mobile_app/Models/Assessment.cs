using System;
using SQLite;

namespace mobile_app.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }// Primary key
        public int CourseId { get; set; }// Foreign key from Course class/table
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreationDate { get; set; }
        public bool StartNotification { get; set; }
        public string Notes { get; set; }
    }
}
