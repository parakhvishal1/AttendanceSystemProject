using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;

namespace AttendanceSystemDemo.Models
{


    public  partial class Student
    {
      

        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Grade { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }


      
       
        

    }
}
