using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSMS_API.Models;

namespace OSMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CourseController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Course.ToListAsync();
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Course/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseID)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Course
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseID }, course);
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }

        // DELETE: api/deneme/5
        [HttpDelete("deneme/{id}")]
        public string  DeleteCourseDeneme(int id)
        {
            // _context.CourseRegistration.FromSqlInterpolated($"Delete From CourseRegistration where CourseID={id}");
            var customer = _context.CourseRegistration.First(c => c.CourseID == id);

            _context.Remove(customer);

            _context.SaveChanges();

            return "silme bloğuna girdi";
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseID == id);
        }

        //Öğretmene ait kurs listesi
        [HttpGet("/api/[controller]/GetCourseByTeacherID/{TeacherID}")]
        public ActionResult GetCourseByTeacherID(int TeacherID)
        {
            return Ok(_context.Course.FromSqlInterpolated($"Select * From Course Where TeacherID={TeacherID}"));
        }

        //Öğrencinin Kayıtlı Olduğu Kurs Listesi
        [HttpGet("/api/[controller]/GetCourseByStudentID/{StudentID}")]
        public ActionResult GetCourseByStudentID(int StudentID)
        {
            return Ok(_context.Course.FromSqlInterpolated($"select * from Course where CourseID in (select CourseID from CourseRegistration where StudentID = {StudentID})"));
        }

        //Öğrencinin Kayıtlı Olmadığı Kurs Listesi
        [HttpGet("/api/[controller]/GetCourseByNotStudentID/{StudentID}")]
        public ActionResult GetCourseByNotStudentID(int StudentID)
        {
            return Ok(_context.Course.FromSqlInterpolated($"select * from Course where CourseID not in (select CourseID from CourseRegistration where StudentID = {StudentID})"));
        }

        //Öğrencinin Kayıtlı Olmadığı Kurs Listesi İsimli
        //[AllowAnonymous]
        //[HttpGet("GetCourseByNotStudentIDName/{StudentID}")]
        //public ActionResult GetCourseByNotStudentIDName(int StudentID)
        //{
        //    var query = from b in _context.Set<Course>()
        //                join p in _context.Set<Teacher>()
        //                on b.CourseID equals p.TeacherID into grouping
        //                from p in grouping.DefaultIfEmpty()
        //                select new { b, p };

        //    //_context.Entry<CourseListNotRegistrationStudent>().HasNoKey().ToView("view_name_that_doesnt_exist");
        //    var data = _context.Context.FromSqlRaw($"Select Course.CourseID as CourseId, Teacher.[Name] as TeacherName, Teacher.[SureName] as TeacherSurname, Course.[Name] as CourseName, Course.Fees as CourseFees, Course.Duration as CourseDuration From Course inner join Teacher on Course.TeacherID=Teacher.TeacherID where CourseID  not in (select CourseID from CourseRegistration where StudentID={StudentID})").IgnoreQueryFilters();
        //    return Ok(data);
        //}

        //Bir Öğretmene Ait Toplam Kurs Sayısı
        [HttpGet("totalCourse/{TeacherID}")]
        public  IActionResult Count(int TeacherID)
        {
            var data =  _context.Course.FromSqlRaw($"select * from Course where TeacherID={TeacherID}").ToList();
            var count = data.Count();
            return Ok(count);
        }

        //Toplam Kurs Sayısı
        [HttpGet("totalCourse")]
        public IActionResult Count()
        {
            var data = _context.Course.FromSqlRaw($"select * from Course").ToList();
            var count = data.Count();
            return Ok(count);
        }
    }
}
