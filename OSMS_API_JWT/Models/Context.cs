using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS_API.Models
{
    public class Context
    {
        [Key]
        public int ContextID { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int CourseID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; }

        public Course Course { get; set; }

    }
}
