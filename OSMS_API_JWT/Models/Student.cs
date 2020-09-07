using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS_API.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string Role { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string SureName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(11)")]
        public string ContactNo { get; set; }

        public virtual List<CourseRegistration> CourseRegistrations { get; set; }

    }
}
