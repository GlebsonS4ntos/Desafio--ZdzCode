using Desafio.BackEnd.Config;
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
    config.UseSqlite(connectionString).UseLazyLoadingProxies();
});

builder.Services.AddScoped<IRepositoryEvent, RepositoryEvent>();
builder.Services.AddScoped<IRepositoryPanelist, RepositoryPanelist>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(config =>
{
    config.AddProfile<AutoMapperConfig>();
}).CreateMapper());

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
