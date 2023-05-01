using CompanyApi.Context;
using CompanyApi.Models;
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
public class HardDriveController : ControllerBase
{
    private readonly HardDriveCompanyContext _context;
    private readonly HardDriveService _hardDriveService;
    private readonly CategoryService _categoryService;
    private readonly ConnectionInterfaceTypeService _connectionInterfaceTypeService;
    
    public HardDriveController(HardDriveCompanyContext context, HardDriveService hardDriveService, CategoryService categoryService, ConnectionInterfaceTypeService connectionInterfaceTypeService)
    {
        _context = context;
        _hardDriveService = hardDriveService;
        _categoryService = categoryService;
        _connectionInterfaceTypeService = connectionInterfaceTypeService;
    }
    
    [HttpGet("GetHardDrive")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<HardDriveM>>> GetHardDrive(string login)
    {
        var currentEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeLogin == login);

        if (currentEmployee == null)
        {
            return NotFound();
        }

        return await _hardDriveService.GetAsync();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<HardDriveM>> PostEmployee(string login, HardDrive hardDrive)
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

        if ((_context.HardDrives?.Any(e => e.DriveName == hardDrive.DriveName)).GetValueOrDefault())
        {
            return Content("Такой товар уже существует");
        }

        HardDriveP hardDriveP = new HardDriveP
        {
            DriveName = hardDrive.DriveName,
            DriveSize = hardDrive.DriveSize,
            DriveTypeId = hardDrive.DriveTypeId,
            ConnectionInterfaceId = hardDrive.ConnectionInterfaceId,
        };

        await _context.HardDrives.AddAsync(hardDriveP);
        await _context.SaveChangesAsync();

        var categoryBsonId =  _categoryService.GetAsync(hardDrive.DriveTypeId).Result.Id;
        var interfaceBsonId =  _connectionInterfaceTypeService.GetAsync(hardDrive.ConnectionInterfaceId).Result.Id;

        HardDriveM hardDriveM = new HardDriveM
        {
            Name = hardDrive.DriveName,
            CategoryId = categoryBsonId,
            ConnectionIntTypeId = interfaceBsonId,
            Count = hardDrive.Count,
            Price = hardDrive.Price,
            ps_id = (int)_context.HardDrives
                .AsNoTracking()
                .Where(h => h.DriveName == hardDrive.DriveName)
                .Select(h => h.SerialNumber)
                .FirstOrDefault()
        };
       
        await _hardDriveService.CreateAsync(hardDriveM);

        return Content("Товар создан");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee(string login, int delHardDriveId)
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
        
        
        var delHardDrive = _context.HardDrives.FirstOrDefault(h => h.SerialNumber == delHardDriveId);
        
        if (delHardDrive == null)
        {
            return Content("Товар не существует");
        }
        
        
        
        _context.HardDrives.Remove(delHardDrive);
        await _context.SaveChangesAsync();

        await _hardDriveService.RemoveAsync(delHardDriveId);
        
        return Content("Товар удален");
    }
}