using CompanyApi;
using CompanyApi.Context;
using CompanyApi.Services;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<CompanyShopDbSettings>(
    builder.Configuration.GetSection("CompanyShopMongoDb"));
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<ConnectionInterfaceTypeService>();
builder.Services.AddSingleton<HardDriveService>();
builder.Services.AddSingleton<EmployeeService>();

builder.Services.AddDbContext<HardDriveCompanyContext>();

builder.Services.AddSingleton(AuthOptions.GetSymmetricSecurityKey());

builder.Services.AddControllers();

const string jwtSchemeName = "JwtBearer";

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = jwtSchemeName;
        options.DefaultChallengeScheme = jwtSchemeName;
    })
    .AddJwtBearer(jwtSchemeName,jwtBearerOptions => 
    {
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
		
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });


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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();