using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CompanyApi.Context;
using CompanyApi.Models;
using CompanyApi.Models.Posgres;
using CompanyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CompanyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly HardDriveCompanyContext _context;
    private readonly EmployeeService _employeeService;

    public LoginController(HardDriveCompanyContext context, EmployeeService employeeService)
    {
        _context = context;
        _employeeService = employeeService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] EmployeeLogin employeeLogin)
    {
        Employee? employeeP;

        await using (HardDriveCompanyContext db = new HardDriveCompanyContext())
        {
            employeeP = db.Employees
                .FromSqlRaw("SELECT * FROM employee WHERE employee_login = {0} AND password = crypt({1}, password)",
                    employeeLogin.Login,
                    employeeLogin.Password)
                .FirstOrDefault();
        }

        if (employeeP == null)
        {
            return NotFound("Не верные данные");
        }

        var employeesM = await _employeeService.GetAsync();
        var employeeM = employeesM.FirstOrDefault(e => e.Login == employeeLogin.Login);

        if (employeeM == null)
        {
            return NotFound("Не верные данные");
        }
        
        var securityKey = AuthOptions.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]{
            new Claim(ClaimTypes.NameIdentifier, employeeP.EmployeeLogin)
        };
                
        var token = new JwtSecurityToken(
            AuthOptions.ISSUER,
            AuthOptions.AUDIENCE,
            claims,
            expires: DateTime.Now.AddHours(12),
            signingCredentials: credentials);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
        });
    }
}