using AutoMapper;
using Microsoft.EntityFrameworkCore;
using monpirtest.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PirDbContext>(
    option => option.UseNpgsql(
        "User ID=admin_pir;Password=123;Host=localhost;Port=5432;" +
        "Database=pir;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Idle Lifetime=60;"),
    ServiceLifetime.Singleton);

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

app.Services.GetRequiredService<PirDbContext>().Database.Migrate();

app.Run();