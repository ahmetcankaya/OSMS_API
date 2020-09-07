using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OSMS_API.Models;
using OSMS_API_JWT.Helpers;

namespace OSMS_API_JWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly AppSettings _appSettings;
        public LoginController(DatabaseContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("admin")]
        public async Task<IActionResult> LoginAdmin(LoginModel model)
        {
            var admin = await _context.Admin.FirstOrDefaultAsync(x => x.UserName == model.UserName && x.Password == model.Password);

            if (admin != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_appSettings.JWT_Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                    new Claim("AdminID", admin.AdminID.ToString()),
                    new Claim(ClaimTypes.Role,admin.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Admin Adı ya da Parola Yanlış." });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("teacher")]
        public async Task<IActionResult> LoginTeacher(LoginModel model)
        {
            var teacher = await _context.Teacher.FirstOrDefaultAsync(x => x.UserName == model.UserName && x.Password == model.Password);

            if (teacher != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_appSettings.JWT_Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                    new Claim("TeacherID", teacher.TeacherID.ToString()),
                    new Claim(ClaimTypes.Role,teacher.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Admin Adı ya da Parola Yanlış." });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("student")]
        public async Task<IActionResult> LoginStudent(LoginModel model)
        {
            var student = await _context.Student.FirstOrDefaultAsync(x => x.UserName == model.UserName && x.Password == model.Password);

            if (student != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_appSettings.JWT_Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                    new Claim("StudentID", student.StudentID.ToString()),
                    new Claim(ClaimTypes.Role,student.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Admin Adı ya da Parola Yanlış." });
            }
        }
    }
}
