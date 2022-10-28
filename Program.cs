using BEDefecto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add conexion a base de datos
builder.Services.AddSqlServer<DefectoContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddDbContext<DefectoContext>(p => p.UseInMemoryDatabase("defecto"));

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

var app = builder.Build();

app.MapGet("/dbconexion", async([FromServices] DefectoContext dbContext) =>
{
   dbContext.Database.EnsureCreated();
   return Results.Ok("Base de datos creada: " + dbContext.Database.IsInMemory());
});

app.MapGet("api/test/products", async([FromServices] DefectoContext dbContext) =>
{
   var products = dbContext.Products.ToList();
   return Results.Ok(products);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
