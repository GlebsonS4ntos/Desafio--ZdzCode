using Desafio.BackEnd.Context;
using Desafio.BackEnd.Interfaces;
using Desafio.BackEnd.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Sqlite");

builder.Services.AddDbContext<DataContext>(config =>
{
    config.UseSqlite(connectionString);
});

builder.Services.AddScoped<IRepositoryEvent, RepositoryEvent>();
builder.Services.AddScoped<IRepositoryPanelist, RepositoryPanelist>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
