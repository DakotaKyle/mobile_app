using System;
using SQLite;

namespace mobile_app.Models
{
    public class Gadget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
