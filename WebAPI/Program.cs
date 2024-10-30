using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using TaskFlow.Infrastructure.Repositories;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión y el DbContext
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar los repositorios
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IClientRepository, ClientRepository>();
// builder.Services.AddScoped<IAdminRepository, AdminRepository>();
// builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Despues de agregar autenticación 
// app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
