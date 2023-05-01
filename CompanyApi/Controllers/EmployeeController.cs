using CompanyApi.Context;
using CompanyApi.Models.Mongo;
using CompanyApi.Models.Posgres;
using CompanyApi.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using Npgsql;

namespace CompanyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly HardDriveCompanyContext _context;
    private readonly EmployeeService _employeeService;

    public EmployeeController(HardDriveCompanyContext context, EmployeeService employeeService)
    {
        _context = context;
        _employeeService = employeeService;
    }

    [HttpGet("GetEmployee")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(string login)
    {
        var currentEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeLogin == login);

        if (currentEmployee == null)
        {
            return NotFound();
        }

        return currentEmployee.PositionId switch
        {
            3 => await _context.Employees
                .Include(e => e.Position)
                .ToListAsync(),
            2 => await _context.Employees
                .Where(e => e.PositionId != 3)
                .Include(e => e.Position)
                .ToListAsync(),
            1 => await _context.Employees
                .Where(e => e.EmployeeLogin == currentEmployee.EmployeeLogin)
                .Include(e => e.Position)
                .ToListAsync()
        };
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Employee>> PostEmployee(string login, Employee employee)
    {
        var currentEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeLogin == login);
        if (currentEmployee == null)
        {
            return NotFound();
        }

        if (currentEmployee.PositionId == 1)
        {
            return Content("Нет доступа");
        }

        if ((_context.Employees?.Any(e => e.EmployeeLogin == employee.EmployeeLogin)).GetValueOrDefault())
        {
            return Content("Пользователь уже существует");
        }

        if (employee.PositionId == 3)
        {
            return Content("Нельзя создать администратора");
        }

        EmployeeM employeeM = new EmployeeM
        {
            FullName = employee.FullName,
            Email = employee.Email,
            Login = employee.EmployeeLogin,
            PhoneNumber = employee.PhoneNumber,
            Position = _context.EmployeesPositions
                .FirstOrDefault(p => p.PositionId == employee.PositionId)?.PositionName
        };

        //
        string q = $@"CALL create_employee(
                @fullName,
                @mail,
                @phone,
                @positionId,
                @login,
                @password);";

        string connectionString =
            "Server=localhost;Port=5432;Database=hard_drive_company;User Id=adef;Password=199as55";
        using (var connection = new NpgsqlConnection(connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("fullName", employee.FullName);
            parameters.Add("mail", employee.Email);
            parameters.Add("phone", employee.PhoneNumber);
            parameters.Add("positionId", employee.PositionId);
            parameters.Add("login", employee.EmployeeLogin);
            parameters.Add("password", employee.Password);

            await connection.QueryAsync(q, parameters).ConfigureAwait(false);
        }

        await _employeeService.CreateAsync(employeeM);

        return Content("Пользователь создан");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee(string login, string delLogin)
    {
        var currentEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeLogin == login);

        if (currentEmployee == null)
        {
            return NotFound();
        }

        if (currentEmployee.PositionId == 1)
        {
            return Content("Нет доступа");
        }

        var delEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeLogin == delLogin);

        if (delEmployee == null)
        {
            return Content("Пользователь не существует");
        }

        if (delEmployee.PositionId == 3)
        {
            return Content("Нельзя удалить администратора");
        }

        _context.Employees.Remove(delEmployee);
        await _context.SaveChangesAsync();

        await _employeeService.RemoveAsync(delLogin);

        return Content("Пользователь удален");
    }
}