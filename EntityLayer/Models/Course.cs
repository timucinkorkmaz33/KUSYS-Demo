using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class Course
    {
         [Key]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
       
 
    }
}
