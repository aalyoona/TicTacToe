using Microsoft.EntityFrameworkCore;
using TicTacToe;
using TicTacToe.BLL;
using TicTacToe.DL;
using TicTacToe.Extensions;
using TicTacToe.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string _connectionStringVariableName = "CONNECTION_STRING";
string conString = builder.Configuration.GetValue<string>(_connectionStringVariableName);

builder.Services.AddDbContext<TicTacToeDbContext>(options =>
            options.UseSqlServer(conString));
builder.Services.AddAutoMapper(typeof(AutoMapperBLL).Assembly, typeof(AutoMapperAPI).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterDependencies();

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

app.UseMiddleware<GlobalExceptionHandler>();

app.Run();
