using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS_API.Models
{
    public class CourseRegistration
    {
        [Key]
        public int CourseRegistrationID { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int CourseID { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int StudentID { get; set; }



    }
}
