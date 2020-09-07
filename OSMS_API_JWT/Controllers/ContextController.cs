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
    public class ContextController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ContextController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Context
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Context>>> GetContext()
        {
            return await _context.Context.ToListAsync();
        }

        // GET: api/Context/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Context>> GetContext(int id)
        {
            var context = await _context.Context.FindAsync(id);

            if (context == null)
            {
                return NotFound();
            }

            return context;
        }

        // PUT: api/Context/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContext(int id, Context context)
        {
            if (id != context.ContextID)
            {
                return BadRequest();
            }

            _context.Entry(context).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContextExists(id))
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

        // POST: api/Context
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Context>> PostContext(Context context)
        {
            _context.Context.Add(context);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContext", new { id = context.ContextID }, context);
        }

        // DELETE: api/Context/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Context>> DeleteContext(int id)
        {
            var context = await _context.Context.FindAsync(id);
            if (context == null)
            {
                return NotFound();
            }

            _context.Context.Remove(context);
            await _context.SaveChangesAsync();

            return context;
        }

        private bool ContextExists(int id)
        {
            return _context.Context.Any(e => e.ContextID == id);
        }


        //Kursa Ait Tüm İçerik
        [HttpGet("/api/[controller]/contents/{CourseID}")]
        public ActionResult GetContextList(int CourseID)
        {
            return Ok(_context.Context.FromSqlInterpolated($"select * from Context where  CourseID = {CourseID}"));
        }

        //Kursa Ait İçerik Sayısı Toplamı
        [HttpGet("/api/[controller]/count/{CourseID}")]
        public ActionResult GetContextCount(int CourseID)
        {
            var data = _context.Context.FromSqlInterpolated($"select * from Context where  CourseID = {CourseID}");
            var count = data.Count();
            return Ok(count);
        }

        //Bir Öğretmene Ait Toplam Paylaştığı içerik
        [HttpGet("totalContext/{TeacherID}")]
        public IActionResult Count(int TeacherID)
        {
            var data = _context.Context.FromSqlRaw($"select * from Context where CourseID in (select CourseID from Course where TeacherID={TeacherID})").ToList();
            var count = data.Count();
            return Ok(count);
        }

        //Toplam İçerik Sayısı
        [HttpGet("totalContext")]
        public IActionResult Count()
        {
            var data = _context.Context.FromSqlRaw($"select * from Context").ToList();
            var count = data.Count();
            return Ok(count);
        }
    }
}
