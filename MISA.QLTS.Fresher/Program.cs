using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using MISA.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IAssetRepo, AssetRepo>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
var app = builder.Build();
app.UseMiddleware<ValidateExceptionMiddleware>();
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
