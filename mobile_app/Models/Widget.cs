using System;
using SQLite;

namespace mobile_app.Models
{
    public class Widget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GadgetId { get; set; }// Foreign key from Gadget class/table
        public string Name { get; set; }
        public string Color { get; set; }
        public bool StartNotification { get; set; }
        public string Notes { get; set; }
    }
}
