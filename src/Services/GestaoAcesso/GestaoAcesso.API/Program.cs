using FluentValidation;
using GestaoAcesso.API.Application.Command;
using GestaoAcesso.API.Filters;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(typeof(CadastrarUsuarioCommandHandler).GetTypeInfo().Assembly);

AssemblyScanner.FindValidatorsInAssembly(typeof(CadastrarUsuarioCommand).Assembly)
  .ForEach(item => builder.Services.AddScoped(item.InterfaceType, item.ValidatorType));

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
