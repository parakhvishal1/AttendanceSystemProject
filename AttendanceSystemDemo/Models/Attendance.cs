using System;
using System.Collections.Generic;

namespace AttendanceSystemDemo.Models
{
    public partial class Attendance
    {
       
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public string Role { get; set; }

       

    }
}
