using CompanyApi;
using CompanyApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<CompanyShopDbSettings>(
    builder.Configuration.GetSection("CompanyShopMongoDb"));
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<ConnectionInterfaceTypeService>();
builder.Services.AddSingleton<HardDriveService>();
builder.Services.AddSingleton<EmployeeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();