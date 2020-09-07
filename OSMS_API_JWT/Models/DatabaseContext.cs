using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS_API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<Student> Student { get; set; }

        public DbSet<Context> Context { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<CourseRegistration> CourseRegistration { get; set; }


    }
}
