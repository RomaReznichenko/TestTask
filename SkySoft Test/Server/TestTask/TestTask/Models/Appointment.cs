using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime Dateendofactuality { get; set; }
        public string Description { get; set; }
        public int Category_id { get; set; }
    }
}
