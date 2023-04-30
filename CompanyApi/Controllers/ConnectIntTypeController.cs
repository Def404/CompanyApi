using CompanyApi.Models.Mongo;
using CompanyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConnectIntTypeController : ControllerBase
{
    private readonly ConnectionInterfaceTypeService _connectionInterfaceTypeService;

    public ConnectIntTypeController(ConnectionInterfaceTypeService connectionInterfaceTypeService) =>
        _connectionInterfaceTypeService = connectionInterfaceTypeService;

    [HttpGet]
    public async Task<List<ConnectionInterfaceType>> Get()
    {
        return await _connectionInterfaceTypeService.GetAsync();
    }
}