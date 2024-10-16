using D_AlturaSystemAPI.Modelos;
using D_AlturaSystemAPI.Servicio;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var MisReglasCors = "ReglasCors";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectSQL")));

builder.Services.AddCors(option => option.AddPolicy(name: MisReglasCors,
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    }
    ));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ServiciosBD>();

var app = builder.Build();

// Sirve archivos estáticos desde wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
// Nota: Swagger se mueve al final para que no sobrescriba la página predeterminada
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(MisReglasCors);
app.MapControllers();

// Swagger y Swagger UI solo si es necesario y en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // Swagger estará disponible en /swagger
    });
}

app.Run();
