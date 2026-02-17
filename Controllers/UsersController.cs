using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using مشروع_قبل_الشغل.Data;

namespace مشروع_قبل_الشغل.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(JwtOpetions jwtOpetions,AppDbContext dbContext) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public ActionResult<string>AuthUser(AuthRequest request)
        {
            var user = dbContext.Set<User>().FirstOrDefault(user => user.Name==request.UserName && 
            user.Password == request.Password);
            if (user == null)
            {
                return Unauthorized();
            } 
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = jwtOpetions.Audience,
                Issuer = jwtOpetions.Issure,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpetions.SigningKey)),
                SecurityAlgorithms.HmacSha256),
               Subject = new ClaimsIdentity( new[]
               {
                   new Claim (ClaimTypes.NameIdentifier,user.Id.ToString()),
                   new Claim(ClaimTypes.Email,"youssef@gmailcom"),
                   new Claim(ClaimTypes.Role,"admin"),
                   new Claim(ClaimTypes.Role,"Superuser"),
                   new Claim("adminsOnly","adminsOnly"),
               }),
            };
            var Securitytoken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(Securitytoken);
            return Ok(accessToken);
        }

    }
}
