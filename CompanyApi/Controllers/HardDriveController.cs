using CompanyApi.Models.Mongo;
using CompanyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HardDriveController :  ControllerBase
{
    private readonly HardDriveService _hardDriveService;

    public HardDriveController(HardDriveService hardDriveService) =>
        _hardDriveService = hardDriveService;
    
    [HttpGet]
    public async Task<List<HardDrive>> Get()
    {
        return await _hardDriveService.GetAsync();
    }
    
    [HttpGet("{categoryId:length(24)}")]
    public async Task<ActionResult<List<HardDrive>>> GetOfCategory(string categoryId)
    {
        var hardDrives = _hardDriveService.GetOfCategoryAsync(categoryId).Result;

        if (hardDrives.Count == 0)
        {
            return NotFound();
        }

        return hardDrives;
    }
    
    [HttpGet("{connectionIntTypeId:length(24)}")]
    public async Task<ActionResult<List<HardDrive>>> GetOfInterfaceType(string connectionIntTypeId)
    {
        var hardDrives = _hardDriveService.GetOfConnectIntAsync(connectionIntTypeId).Result;

        if (hardDrives.Count == 0)
        {
            return NotFound();
        }

        return hardDrives;
    }
    
    [HttpGet("{keyword}")]
    public async Task<ActionResult<List<HardDrive>>> GetOfKeyword(string keyword)
    {
        var hardDrives = _hardDriveService.GetOfKeyword(keyword).Result;

        if (hardDrives.Count == 0)
        {
            return NotFound();
        }

        return hardDrives;
    }
    
    [HttpGet("price")]
    public async Task<ActionResult<List<HardDrive>>> GetOfPrice(int? priceStart, int? priceEnd)
    {
        priceStart ??= 0;
        priceEnd ??= Int32.MaxValue;
        
        var hardDrives = _hardDriveService.GetOfPrice(priceStart, priceEnd).Result;

        if (hardDrives.Count == 0)
        {
            return NotFound();
        }

        return hardDrives;
    }
    
    [HttpGet("{size}")]
    public async Task<ActionResult<List<HardDrive>>> GetOfSize(int size)
    {
        var hardDrives = _hardDriveService.GetOfSize(size).Result;

        if (hardDrives.Count == 0)
        {
            return NotFound();
        }

        return hardDrives;
    }
}