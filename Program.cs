//using BEDefecto;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//
//var builder = WebApplication.CreateBuilder(args);
//
//// Add services to the container.
//
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
////add conexion a base de datos
//builder.Services.AddSqlServer<DefectoContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
//
//builder.Services.AddDbContext<DefectoContext>(p => p.UseInMemoryDatabase("defecto"));
//
//var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy  =>
//                      {
//                          policy.WithOrigins("http://localhost:4200");
//                      });
//});
//
// builder.Services.AddCors(options =>
//            {
//                options.AddPolicy("CorsPolicy",
//                    builder => builder.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader());
//            });
//
//var app = builder.Build();
//
//app.MapGet("/dbconexion", async([FromServices] DefectoContext dbContext) =>
//{
//   dbContext.Database.EnsureCreated();
//   return Results.Ok("Base de datos creada: " + dbContext.Database.IsInMemory());
//});
//
//builder.Services.AddDbContext<DefectoContext>(p => p.UseInMemoryDatabase("defecto"));
//
//
//app.MapGet("api/test/products", async([FromServices] DefectoContext dbContext) =>
//{
//   var products = dbContext.Products.ToList();
//   return Results.Ok(products);
//});
//
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseCors("CorsPolicy");
//
//app.UseHttpsRedirection();
//
//app.UseCors(MyAllowSpecificOrigins);
//
//app.UseAuthorization();
//
//app.MapControllers();
//
//app.Run();
using BEDefecto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add in-memory database provider
builder.Services.AddDbContext<DefectoContext>(options =>
    options.UseInMemoryDatabase(databaseName: "MyInMemoryDatabase"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
