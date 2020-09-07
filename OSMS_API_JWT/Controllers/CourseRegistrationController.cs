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
    public class CourseRegistrationController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CourseRegistrationController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/CourseRegistration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseRegistration>>> GetCourseRegistration()
        {
            return await _context.CourseRegistration.ToListAsync();
        }

        // GET: api/CourseRegistration/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseRegistration>> GetCourseRegistration(int id)
        {
            var courseRegistration = await _context.CourseRegistration.FindAsync(id);

            if (courseRegistration == null)
            {
                return NotFound();
            }

            return courseRegistration;
        }

        // PUT: api/CourseRegistration/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseRegistration(int id, CourseRegistration courseRegistration)
        {
            if (id != courseRegistration.CourseRegistrationID)
            {
                return BadRequest();
            }

            _context.Entry(courseRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseRegistrationExists(id))
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

        // POST: api/CourseRegistration
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CourseRegistration>> PostCourseRegistration(CourseRegistration courseRegistration)
        {
            _context.CourseRegistration.Add(courseRegistration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseRegistration", new { id = courseRegistration.CourseRegistrationID }, courseRegistration);
        }

        // DELETE: api/CourseRegistration/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseRegistration>> DeleteCourseRegistration(int id)
        {
            var courseRegistration = await _context.CourseRegistration.FindAsync(id);
            if (courseRegistration == null)
            {
                return NotFound();
            }

            _context.CourseRegistration.Remove(courseRegistration);
            await _context.SaveChangesAsync();

            return courseRegistration;
        }

        private bool CourseRegistrationExists(int id)
        {
            return _context.CourseRegistration.Any(e => e.CourseRegistrationID == id);
        }


        //Kursa Kayıtlı Öğrenciler
        [HttpGet("/api/[controller]/students/{CourseID}")]
        public ActionResult GetCourseStudentRegistration(int CourseID)
        {
            return Ok(_context.Student.FromSqlInterpolated($"select * from Student where StudentID in (select StudentID from CourseRegistration where CourseID = {CourseID})"));
        }

        //Bir Öğretmene Ait Toplam Kayıtlı Öğrenci Sayısı
        [HttpGet("totalStudent/{TeacherID}")]
        public IActionResult Count(int TeacherID)
        {
            var data = _context.CourseRegistration.FromSqlRaw($"select * from CourseRegistration where CourseID in (select CourseID from Course where TeacherID={TeacherID})").ToList();
            var count = data.Count();
            return Ok(count);
        }

        //Toplam Kayıtlı Öğrenci Sayısı
        [HttpGet("totalStudent")]
        public IActionResult Count()
        {
            var data = _context.CourseRegistration.FromSqlRaw($"select * from CourseRegistration").ToList();
            var count = data.Count();
            return Ok(count);
        }

    }
}
