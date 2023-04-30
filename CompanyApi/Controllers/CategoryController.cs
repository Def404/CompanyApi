using CompanyApi.Models.Mongo;
using CompanyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController :  ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService) =>
        _categoryService = categoryService;

    [HttpGet]
    public async Task<List<Category>> Get()
    {
        return await _categoryService.GetAsync();
    }
}