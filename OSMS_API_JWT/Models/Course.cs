using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS_API.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int TeacherID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public decimal Fees { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Duration { get; set; }

        public Teacher Teacher { get; set; }

        public virtual List<Context> Contexts { get; set; }

        public virtual List<CourseRegistration> CourseRegistrations { get; set; }


    }
}
